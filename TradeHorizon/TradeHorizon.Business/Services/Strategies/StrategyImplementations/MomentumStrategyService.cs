using Microsoft.AspNetCore.SignalR.Client;
using TradeHorizon.Domain.Models.Strategies;
using TradeHorizon.Domain.Constants;
using TradeHorizon.Domain.Interfaces.Strategies;

namespace TradeHorizon.Business.Services.Strategies
{
    /// <summary>
    ///  Momentum Strategy Service (Trades	Trade when volume and price both surge in one direction)
    /// --------------------------
    /// Detects momentum shifts based on user-defined settings (threshold %, sliding window size, price source, volume confirmation).
    /// Monitors real-time candlestick data (from both WebSocket and REST APIs) to identify upward or downward momentum movements.
    /// The strategy allows for both quick momentum detection and confirmation using average price or the most recent candle data.
    /// On detection, triggers an event to broadcast momentum details (direction, price, timestamp) to SignalR clients and other services.
    /// </summary>

    public class MomentumStrategyService
    {
        private readonly List<Candlestick> _candles = [];
        private readonly IStrategiesBroadcaster _strategiesBroadcaster;
        public event Action<string, MomentumDirection, decimal?, long>? MomentumDetected;
        public MomentumSettingsModel _settings = new();

        public MomentumStrategyService(IStrategiesBroadcaster strategiesBroadcaster)
        {
            _strategiesBroadcaster = strategiesBroadcaster;
            MomentumDetected += async (contract, direction, price, timestamp) =>
            {
                await _strategiesBroadcaster.BroadcastMomentumStrategyAsync(contract, direction, price, timestamp);
            };
        }

        public async Task ExecuteStrategyAsync(MomentumSettingsModel momentumSettings)
        {
            _settings = momentumSettings;
            try
            {
                var restApiHubConnection = new HubConnectionBuilder()
                    .WithUrl(StrategyListeningEndpoints.RestOHLCVHubUrl)
                    .WithAutomaticReconnect()
                    .Build();

                var webSocketHubConnection = new HubConnectionBuilder()
                    .WithUrl(StrategyListeningEndpoints.WsCandlestickHubUrl)
                    .WithAutomaticReconnect()
                    .Build();

                restApiHubConnection.On<List<OHLCVModel>>(SignalRConstants.ReceiveOHLCV, candles =>
                {
                    foreach (var candle in candles)
                    {
                        AddCandle(Restcandle: candle);
                    }
                    restApiHubConnection.Remove(SignalRConstants.ReceiveOHLCV);
                });

                await restApiHubConnection.StartAsync();
                await restApiHubConnection.InvokeAsync("JoinGroup", SignalRConstants.OHLCVGroup);

                webSocketHubConnection.On<string>(SignalRConstants.ReceiveCandlestickData, candle =>
                {
                    AddCandle(candle: candle);
                });

                await webSocketHubConnection.StartAsync();
                await webSocketHubConnection.InvokeAsync("Subscribe", string.Empty, SignalRConstants.CandlestickGroupWS);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private void AddCandle(string candle = "", OHLCVModel? Restcandle = null)
        {
            Candlestick data = new();
            try
            {
                if (Restcandle == null)
                {
                    var webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<CandlestickModel>(candle);
                    if (webSocketMessage?.Result is not ResultData<CandlestickModel> { Data.Count: > 0 } candlestickModels)
                        return;

                    var candleData = candlestickModels.Data[0];
                    data = new Candlestick
                    {
                        Timestamp = candleData?.Time ?? 0,
                        Symbol = _settings.Contract,
                        Open = ShortHandFuncUtils.StringToDecimal(candleData?.Open ?? "0"),
                        High = ShortHandFuncUtils.StringToDecimal(candleData?.High ?? "0"),
                        Low = ShortHandFuncUtils.StringToDecimal(candleData?.Low ?? "0"),
                        Close = ShortHandFuncUtils.StringToDecimal(candleData?.Close ?? "0"),
                        Volume = candleData?.Volume ?? 0
                    };
                }
                else
                {
                    data = new Candlestick
                    {
                        Timestamp = (long)(Restcandle.UnixTimeStamp ?? 0),
                        Symbol = _settings.Contract,
                        Open = ShortHandFuncUtils.StringToDecimal(Restcandle.OpenPrice ?? "0"),
                        High = ShortHandFuncUtils.StringToDecimal(Restcandle.HighPrice ?? "0"),
                        Low = ShortHandFuncUtils.StringToDecimal(Restcandle.LowPrice ?? "0"),
                        Close = ShortHandFuncUtils.StringToDecimal(Restcandle.ClosePrice ?? "0"),
                        Volume = Restcandle?.Volume ?? 0
                    };
                }

                _candles.Add(data);

                if (_candles.Count > 1000)
                {
                    _candles.RemoveAt(0);
                }

                CheckForMomentum(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private void CheckForMomentum(Candlestick latestCandle)
        {
            decimal priceChangePercent = decimal.Zero;
            try
            {
                var recentCandles = _candles.TakeLast(_settings.SlidingWindow + 1).SkipLast(1); // Exclude the latest candle from sliding window to avoid self-referencing in breakout detection.

                if (!recentCandles.Any()) return;

                decimal priceToCheck = _settings.PriceSourceToCheck switch
                {
                    PriceSource.Open => latestCandle.Open,
                    PriceSource.High => latestCandle.High,
                    PriceSource.Low => latestCandle.Low,
                    _ => latestCandle.Close,
                };

                if (_settings.IsAvgPriceUsing)
                {
                    decimal avgPrice = recentCandles.Average(c => c.Close);
                    priceChangePercent = (priceToCheck - avgPrice) / avgPrice * 100;
                }
                else
                {
                    var lastCandle = recentCandles.Last();
                    decimal startPrice = _settings.PriceSourceToCheck switch
                    {
                        PriceSource.Open => lastCandle.Open,
                        PriceSource.High => lastCandle.High,
                        PriceSource.Low => lastCandle.Low,
                        _ => lastCandle.Close,
                    };

                    if (startPrice == 0) return;
                    priceChangePercent = (priceToCheck - startPrice) / startPrice * 100;
                }


                bool isMomentum = false;
                MomentumDirection momentumDirection = MomentumDirection.Neautral;

                if (priceChangePercent >= _settings.ThresholdPercent)
                {
                    isMomentum = true;
                    momentumDirection = MomentumDirection.Up;
                }
                else if (priceChangePercent <= -_settings.ThresholdPercent)
                {
                    isMomentum = true;
                    momentumDirection = MomentumDirection.Down;
                }

                if (isMomentum && _settings.VolumeConfirmationRequired)
                {
                    var avgVolume = recentCandles.Average(c => c.Volume);
                    if (latestCandle.Volume < avgVolume * _settings.VolumeMultiplier)
                    {
                        isMomentum = false;
                    }
                }

                if (isMomentum)
                    OnMomentumDetected(_settings.Contract, momentumDirection, priceToCheck, latestCandle.Timestamp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }


        private void OnMomentumDetected(string contract, MomentumDirection direction, decimal? price, long timestamp)
        {
            MomentumDetected?.Invoke(contract, direction, price, timestamp);
        }
    }
}
