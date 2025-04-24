using Microsoft.AspNetCore.SignalR.Client;
using TradeHorizon.Domain.Models.Strategies;
using TradeHorizon.Domain.Constants;
using TradeHorizon.Domain.Interfaces.Strategies;

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
        public event Action<BreakoutDirection, decimal?, long>? BreakoutDetected;
        public BreakoutSettingsModel _settings = new();

        public BreakoutStrategyService(IStrategiesBroadcaster strategiesBroadcaster)
        {
            _strategiesBroadcaster = strategiesBroadcaster;
            BreakoutDetected += async (direction, price, timestamp) =>
            {
                await _strategiesBroadcaster.BroadcastBreakoutStrategyAsync(direction, price, timestamp);
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

                restApiHubConnection.On<List<Candlestick>>(SignalRConstants.ReceiveOHLCV, candles =>
                {
                    foreach (var candle in candles)
                    {
                        AddCandle(candle);
                    }
                });

                await restApiHubConnection.StartAsync();

                webSocketHubConnection.On<Candlestick>(SignalRConstants.ReceiveCandlestickData, candle =>
                {
                    AddCandle(candle);
                });

                await webSocketHubConnection.StartAsync();
            }
            catch
            {
                throw;
            }
        }

        private void AddCandle(Candlestick candle)
        {
            try
            {
                _candles.Add(candle);

                if (_candles.Count > 1000)
                {
                    _candles.RemoveAt(0);
                }

                CheckForBreakout(candle);
            }
            catch
            {
                throw;
            }
        }

        private void CheckForBreakout(Candlestick latestCandle)
        {
            try
            {
                var recentCandles = _candles.TakeLast(_settings.SlidingWindow);

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
                    OnBreakoutDetected(breakoutDirection, priceToCheck, latestCandle.Timestamp);
            }
            catch
            {
                throw;
            }
        }

        private void OnBreakoutDetected(BreakoutDirection direction, decimal? price, long timestamp)
        {
            BreakoutDetected?.Invoke(direction, price, timestamp);
        }
    }
}
