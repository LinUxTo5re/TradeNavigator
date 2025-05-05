using Microsoft.AspNetCore.SignalR.Client;
using TradeHorizon.Domain.Models.Strategies;
using TradeHorizon.Domain.Constants;
using TradeHorizon.Domain.Interfaces.Strategies;

namespace TradeHorizon.Business.Services.Strategies
{
    /// <summary>
    ///  Reversal Strategy Service (Detect sudden reversals via wicks and liquidations)
    /// --------------------------
    /// Monitors real-time candlestick and liquidation data to detect potential price reversals.
    /// A reversal signal is generated if a candle wick exceeds a user-defined wick threshold percentage,
    /// or if a liquidation event surpasses a volume threshold.
    /// On detection, triggers an event to broadcast reversal details to SignalR clients and other services.
    /// </summary>

    public class ReversalStrategyService
    {
        private readonly List<Candlestick> _candles = [];
        private readonly IStrategiesBroadcaster _strategiesBroadcaster;
        public event Action<string, ReversalDirection, decimal?, long>? ReversalDetected;
        public ReversalSettingsModel _settings = new();

        public ReversalStrategyService(IStrategiesBroadcaster strategiesBroadcaster)
        {
            _strategiesBroadcaster = strategiesBroadcaster;
            ReversalDetected += async (contract, direction, price, timestamp) =>
            {
                await _strategiesBroadcaster.BroadcastReversalStrategyAsync(contract, direction, price, timestamp);
            };
        }

        public async Task ExecuteStrategyAsync(ReversalSettingsModel reversalSettings)
        {
            _settings = reversalSettings;
            try
            {
                var candlestickHubConnection = new HubConnectionBuilder()
                    .WithUrl(StrategyListeningEndpoints.WsCandlestickHubUrl)
                    .WithAutomaticReconnect()
                    .Build();

                var liquidationHubConnection = new HubConnectionBuilder()
                    .WithUrl(StrategyListeningEndpoints.WsLiquidationHubUrl)
                    .WithAutomaticReconnect()
                    .Build();

                candlestickHubConnection.On<string>(SignalRConstants.ReceiveCandlestickData, candle =>
                {
                    AddCandle(candle: candle);
                });

                liquidationHubConnection.On<string>(SignalRConstants.ReceiveLiquidationData, liquidation =>
                {
                    HandleLiquidation(liquidation);
                });

                await candlestickHubConnection.StartAsync();
                await liquidationHubConnection.StartAsync();

                await candlestickHubConnection.InvokeAsync("Subscribe", string.Empty, SignalRConstants.CandlestickGroupWS);
                await liquidationHubConnection.InvokeAsync("Subscribe", string.Empty, SignalRConstants.LiquidationGroupWS);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private void AddCandle(string candle)
        {
            try
            {
                var webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<CandlestickModel>(candle);
                if (webSocketMessage?.Result is not ResultData<CandlestickModel> { Data.Count: > 0 } candlestickModels)
                    return;

                var candleData = candlestickModels.Data[0];
                var data = new Candlestick
                {
                    Timestamp = candleData?.Time ?? 0,
                    Symbol = _settings.Contract,
                    Open = ShortHandFuncUtils.StringToDecimal(candleData?.Open ?? "0"),
                    High = ShortHandFuncUtils.StringToDecimal(candleData?.High ?? "0"),
                    Low = ShortHandFuncUtils.StringToDecimal(candleData?.Low ?? "0"),
                    Close = ShortHandFuncUtils.StringToDecimal(candleData?.Close ?? "0"),
                    Volume = candleData?.Volume ?? 0
                };

                _candles.Add(data);

                if (_candles.Count > 1000)
                {
                    _candles.RemoveAt(0);
                }

                CheckForReversal(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private void HandleLiquidation(string liquidation)
        {
            try
            {
                var webSocketMessage = WebSocketMessageDeserializer.DeserializeWithResultData<LiquidationModel>(liquidation);
                if (webSocketMessage?.Result is not ResultData<LiquidationModel> { Data.Count: > 0 } liquidationModels)
                    return;

                var liquidationData = liquidationModels.Data[0];
                decimal liquidationVolume = liquidationData?.Quantity ?? 0;

                if (liquidationVolume >= _settings.LiquidationVolumeThreshold)
                {
                    var direction = liquidationData?.Side?.ToLower() == "buy" ? ReversalDirection.Up : ReversalDirection.Down;
                    var price = ShortHandFuncUtils.StringToDecimal(liquidationData?.Price ?? "0");
                    var timestamp = liquidationData?.Timestamp ?? DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                    OnReversalDetected(_settings.Contract, direction, price, timestamp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private void CheckForReversal(Candlestick latestCandle)
        {
            try
            {
                decimal candleHeight = latestCandle.High - latestCandle.Low;
                if (candleHeight == 0) return;

                decimal upperWick = latestCandle.High - Math.Max(latestCandle.Open, latestCandle.Close);
                decimal lowerWick = Math.Min(latestCandle.Open, latestCandle.Close) - latestCandle.Low;

                decimal upperWickPercent = (upperWick / candleHeight) * 100;
                decimal lowerWickPercent = (lowerWick / candleHeight) * 100;

                if (upperWickPercent >= _settings.WickThresholdPercent)
                {
                    OnReversalDetected(_settings.Contract, ReversalDirection.Down, latestCandle.High, latestCandle.Timestamp);
                }
                else if (lowerWickPercent >= _settings.WickThresholdPercent)
                {
                    OnReversalDetected(_settings.Contract, ReversalDirection.Up, latestCandle.Low, latestCandle.Timestamp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        private void OnReversalDetected(string contract, ReversalDirection direction, decimal? price, long timestamp)
        {
            ReversalDetected?.Invoke(contract, direction, price, timestamp);
        }
    }
}
