using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Models.Strategies;
using TradeHorizon.Business.Services.Strategies;

namespace TradeHorizon.API.Hubs.Strategies
{
    public class BreakoutStrategyHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;

        public BreakoutStrategyHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Subscribe(BreakoutStrategySettingsDto breakoutSettings)
        {
            ArgumentNullException.ThrowIfNull(breakoutSettings, nameof(breakoutSettings));

            if (!Enum.TryParse(breakoutSettings.PriceSource, true, out PriceSource priceSource))
            {
                throw new ArgumentException($"Invalid PriceSource value: {breakoutSettings.PriceSource}");
            }

            try
            {
                var breakoutSettingsMap = new BreakoutSettingsModel
                {
                    Contract = breakoutSettings.Contract,
                    ThresholdPercent = breakoutSettings.ThresholdPercent,
                    SlidingWindow = breakoutSettings.SlidingWindow,
                    PriceSourceToCheck = priceSource,
                    VolumeConfirmationRequired = breakoutSettings.VolumeConfirmationRequired,
                    VolumeMultiplier = breakoutSettings.VolumeMultiplier
                };

                var breakoutStrategyService = _serviceProvider.GetRequiredService<BreakoutStrategyService>();

                await breakoutStrategyService.ExecuteStrategyAsync(breakoutSettingsMap);

                await Groups.AddToGroupAsync(Context.ConnectionId, breakoutSettings.GroupName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

    }
}