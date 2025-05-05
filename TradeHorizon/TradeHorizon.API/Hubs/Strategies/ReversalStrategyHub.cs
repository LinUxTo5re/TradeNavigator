using Microsoft.AspNetCore.SignalR;
using TradeHorizon.Domain.Models.Strategies;
using TradeHorizon.Business.Services.Strategies;

namespace TradeHorizon.API.Hubs.Strategies
{
    public class ReversalStrategyHub : Hub
    {
        private readonly IServiceProvider _serviceProvider;

        public ReversalStrategyHub(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Subscribe(MomentumStrategySettingsDto momentumSettings)
        {
            ArgumentNullException.ThrowIfNull(momentumSettings, nameof(momentumSettings));

            if (!Enum.TryParse(momentumSettings.PriceSource, true, out PriceSource priceSource))
            {
                throw new ArgumentException($"Invalid PriceSource value: {momentumSettings.PriceSource}");
            }

            try
            {
                var momentumSettingsModel = new MomentumSettingsModel
                {
                    Contract = momentumSettings.Contract,
                    ThresholdPercent = momentumSettings.ThresholdPercent,
                    SlidingWindow = momentumSettings.SlidingWindow,
                    PriceSourceToCheck = priceSource,
                    VolumeConfirmationRequired = momentumSettings.VolumeConfirmationRequired,
                    VolumeMultiplier = momentumSettings.VolumeMultiplier,
                    IsAvgPriceUsing = momentumSettings.IsAvgPriceUsing,
                };

                var momentumStrategyService = _serviceProvider.GetRequiredService<MomentumStrategyService>();

                await momentumStrategyService.ExecuteStrategyAsync(momentumSettingsModel);

                await Groups.AddToGroupAsync(Context.ConnectionId, momentumSettings.GroupName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

    }
}