using TradeHorizon.Domain.Interfaces.RestAPI;
using Microsoft.AspNetCore.SignalR;
using TradeHorizon.API.Hubs.RestAPI;
using TradeHorizon.Domain.Constants;

public class GateioBroadcaster : IGateioBroadcaster
{
    private readonly IHubContext<OhlcvHub> _ohlcvHubContext;
    private readonly IHubContext<FundingRateHub> _fundingRateHubContext;
    private readonly IHubContext<ContractStatsHub> _contractStatsHubContext;
    private readonly IHubContext<OrderBookHub> _orderBookHubContext;
    private readonly IHubContext<LiqOrdersHub> _liqOrdersHubContext;

    public GateioBroadcaster(
        IHubContext<OhlcvHub> ohlcvHubContext,
        IHubContext<FundingRateHub> fundingRateHubContext,
        IHubContext<ContractStatsHub> contractStatsHubContext,
        IHubContext<OrderBookHub> orderBookHubContext,
        IHubContext<LiqOrdersHub> liqOrdersHubContext)
    {
        _ohlcvHubContext = ohlcvHubContext;
        _fundingRateHubContext = fundingRateHubContext;
        _contractStatsHubContext = contractStatsHubContext;
        _orderBookHubContext = orderBookHubContext;
        _liqOrdersHubContext = liqOrdersHubContext;
    }

    public async Task BroadcastOHLCVAsync(object? data)
    {
        await _ohlcvHubContext.Clients.Group(SignalRConstants.OHLCVGroup).SendAsync(SignalRConstants.ReceiveOHLCV, data);
    }

    public async Task BroadcastFundingRateAsync(object? data)
    {
        await _fundingRateHubContext.Clients.Group(SignalRConstants.FundingRateGroup).SendAsync(SignalRConstants.ReceiveFundingRate, data);
    }

    public async Task BroadcastContractStatsAsync(object? data)
    {
        await _contractStatsHubContext.Clients.Group(SignalRConstants.ContractStatsGroup).SendAsync(SignalRConstants.ReceiveContractStats, data);
    }

    public async Task BroadcastOrderBookAsync(object? data)
    {
        await _orderBookHubContext.Clients.Group(SignalRConstants.OrderBookGroup).SendAsync(SignalRConstants.ReceiveOrderBook, data);
    }

    public async Task BroadcastLiqOrdersAsync(object? data)
    {
        await _liqOrdersHubContext.Clients.Group(SignalRConstants.LiqOrdersGroup).SendAsync(SignalRConstants.ReceiveLiqOrders, data);
    }

}