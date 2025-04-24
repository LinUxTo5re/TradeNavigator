using Microsoft.AspNetCore.SignalR.Client;
using TradeHorizon.Domain.Models.Strategies;
using TradeHorizon.Domain.Constants;
using TradeHorizon.Domain.Interfaces.Strategies;
using System.Text.Json;

namespace TradeHorizon.Business.Services.Strategies
{
    /// <summary>
    ///  Breakout Strategy Service
    ///--------------------------
    // Detects price breakouts based on user-defined settings (threshold %, sliding window size, price source, volume confirmation).
    // Monitors real-time candlestick data to identify upward or downward breakouts.
    // On detection, triggers an event to broadcast breakout details to SignalR clients and other services.
    /// </summary>
    public class BreakoutStrategyService
    {
        private readonly List<Candlestick> _candles = [];
        private readonly IStrategiesBroadcaster _strategiesBroadcaster;
        public event Action<string, BreakoutDirection, decimal?, long>? BreakoutDetected;
        public BreakoutSettingsModel _settings = new();

        public BreakoutStrategyService(IStrategiesBroadcaster strategiesBroadcaster)
        {
            _strategiesBroadcaster = strategiesBroadcaster;
            BreakoutDetected += async (contract, direction, price, timestamp) =>
            {
                await _strategiesBroadcaster.BroadcastBreakoutStrategyAsync(contract, direction, price, timestamp);
            };
        }

        public async Task ExecuteStrategyAsync(BreakoutSettingsModel breakoutSettings)
        {
            _settings = breakoutSettings;
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
                if (Restcandle == null) // WS data
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
                else // REST data
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

                CheckForBreakout(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private void CheckForBreakout(Candlestick latestCandle)
        {
            try
            {
                var recentCandles = _candles.TakeLast(_settings.SlidingWindow + 1).SkipLast(1); // Exclude the latest candle from sliding window to avoid self-referencing in breakout detection.

                var highestHigh = recentCandles.Max(c => c.High);
                var lowestLow = recentCandles.Min(c => c.Low);

                var upperBreakoutLevel = highestHigh * (1 + _settings.ThresholdPercent / 100);
                var lowerBreakoutLevel = lowestLow * (1 - _settings.ThresholdPercent / 100);

                decimal priceToCheck = _settings.PriceSourceToCheck switch
                {
                    PriceSource.Open => latestCandle.Open,
                    PriceSource.High => latestCandle.High,
                    PriceSource.Low => latestCandle.Low,
                    _ => latestCandle.Close,
                };

                bool isBreakout = false;
                BreakoutDirection breakoutDirection = BreakoutDirection.Down;

                if (priceToCheck > upperBreakoutLevel)
                {
                    isBreakout = true;
                    breakoutDirection = BreakoutDirection.Up;
                }
                else if (priceToCheck < lowerBreakoutLevel)
                {
                    isBreakout = true;
                    breakoutDirection = BreakoutDirection.Down;
                }

                if (isBreakout && _settings.VolumeConfirmationRequired)
                {
                    var avgVolume = recentCandles.Average(c => c.Volume);
                    if (latestCandle.Volume < avgVolume * _settings.VolumeMultiplier)
                    {
                        isBreakout = false;
                    }
                }

                if (isBreakout)
                    OnBreakoutDetected(_settings.Contract, breakoutDirection, priceToCheck, latestCandle.Timestamp);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private void OnBreakoutDetected(string contract, BreakoutDirection direction, decimal? price, long timestamp)
        {
            BreakoutDetected?.Invoke(contract, direction, price, timestamp);
        }
    }
}
