using TradeHorizon.Business.Interfaces.RestAPI;
using TradeHorizon.Business.Services.RestAPI;
using TradeHorizon.DataAccess.Interfaces.RestAPI;
using TradeHorizon.DataAccess.Repositories.RestAPI;
using TradeHorizon.API.Hubs.RestAPI;
using TradeHorizon.API.Hubs;
using TradeHorizon.DataAccess.Repositories.Websocket;
using TradeHorizon.Domain.Interfaces.Websockets;
using TradeHorizon.Business.Services.Websocket;
using TradeHorizon.Domain.Interfaces.RestAPI;
using TradeHorizon.Business.Services.BackgroundService;
using TradeHorizon.Domain.Interfaces.Strategies;
using TradeHorizon.Business.Services.Strategies;
using TradeHorizon.API.Hubs.Strategies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();

#region Background services (start on app)
// skipping hosted services during development
// builder.Services.AddHostedService<FilterContractWorker>();
#endregion

#region DI container REST
builder.Services.AddScoped<IGateioBroadcaster, GateioBroadcaster>();
builder.Services.AddScoped<ICoinalyzeRepository, CoinalyzeRepository>();
builder.Services.AddScoped<ICoinalyzeService, CoinalyzeService>();
builder.Services.AddScoped<IGateioRepository, GateioRepository>();
builder.Services.AddScoped<IGateioService, GateioService>();
builder.Services.AddScoped<IFilterContractService, FilterContractService>();
builder.Services.AddScoped<IFilterContractRepository, FilterContractRepository>();
#endregion

#region DI container WS 
#region Ticker
builder.Services.AddSingleton<IGateTickerBroadcaster, GateTickerBroadcaster>();  // Broadcaster
builder.Services.AddSingleton<IGateTickerProcessor, GateTickerService>();

builder.Services.AddSingleton<IGateTradesClient>(provider =>
    new GateTradeClient(
        provider.GetRequiredService<IGateTradesProcessor>(),
        new GateWebSocketClient(
            provider.GetRequiredService<IGateTradesProcessor>()
        )
    ));

#endregion

#region Trades
builder.Services.AddSingleton<IGateTradesBroadcaster, GateTradeBroadcaster>();  // Broadcaster
builder.Services.AddSingleton<IGateTradesProcessor, GateTradesService>();

builder.Services.AddSingleton<IGateTickerClient>(provider =>
    new GateTickerClient(
        provider.GetRequiredService<IGateTickerProcessor>(),
        new GateWebSocketClient(
            provider.GetRequiredService<IGateTickerProcessor>()
        )
    ));
#endregion

#region  Candlesticks
builder.Services.AddSingleton<IGateCandlesticksBroadcaster, GateCandlesticksBroadcaster>();
builder.Services.AddSingleton<IGateCandlesticksProcessor, GateCandlesticksService>();

builder.Services.AddSingleton<IGateCandlestickClient>(provider =>
    new GateCandlesticksClient(
        provider.GetRequiredService<IGateCandlesticksProcessor>(),
        new GateWebSocketClient(
            provider.GetRequiredService<IGateCandlesticksProcessor>()
        )
    ));
#endregion

#region Public Liquidates
builder.Services.AddSingleton<IGatePublicLiquidatesBroadcaster, GatePublicLiquidatesBroadcaster>();
builder.Services.AddSingleton<IGatePublicLiquidatesProcessor, GatePublicLiquidatesService>();

builder.Services.AddSingleton<IGatePublicLiquidatesClient>(provider =>
new GatePublicLiquidatesClient(
    provider.GetRequiredService<IGatePublicLiquidatesProcessor>(),
    new GateWebSocketClient(
        provider.GetRequiredService<IGatePublicLiquidatesProcessor>()
    )
));
#endregion

#region Contract Stats
builder.Services.AddSingleton<IGateContractStatsBroadcaster, GateContractStatsBroadcaster>();
builder.Services.AddSingleton<IGateContractStatsProcessor, GateContractStatsService>();

builder.Services.AddSingleton<IGateContractStatsClient>(provider =>
new GateContractStatsClient(
    provider.GetRequiredService<IGateContractStatsProcessor>(),
    new GateWebSocketClient(
        provider.GetRequiredService<IGateContractStatsProcessor>()
    )
));

#endregion

#region Order book update
builder.Services.AddSingleton<IGateOrderBookUpdateBroadcaster, GateOrderBookUpdateBroadcaster>();
builder.Services.AddSingleton<IGateOrderBookUpdateProcessor, GateOrderBookUpdateService>();

builder.Services.AddSingleton<IGateOrderBookUpdateClient>(provider =>
new GateOrderBookUpdateClient(
    provider.GetRequiredService<IGateOrderBookUpdateProcessor>(),
    new GateWebSocketClient(
        provider.GetRequiredService<IGateOrderBookUpdateProcessor>()
    )
));
#endregion
#endregion

#region DI container Strategies
builder.Services.AddScoped<BreakoutStrategyService>();
builder.Services.AddScoped<IStrategiesBroadcaster, StrategiesBroadcaster>();
#endregion

#region CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:8000", "https://localhost:7001")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
#endregion

var app = builder.Build();

#region Hubs
#region WS hub
app.MapHub<GateTickerHub>("/hub/ws/gate-ticker");
app.MapHub<GateTradesHub>("/hub/ws/gate-trades");
app.MapHub<GateCandlesticksHub>("/hub/ws/gate-candlesticks");
app.MapHub<GatePublicLiquidatesHub>("/hub/ws/gate-pliq-orders");
app.MapHub<GateContractStatsHub>("/hub/ws/gate-contract-stats");
app.MapHub<GateOrderBookUpdateHub>("/hub/ws/gate-order-book");
#endregion

#region RestAPI hub
app.MapHub<OhlcvHub>("/hub/rest/ohlcv");
app.MapHub<FundingRateHub>("/hub/rest/frate");
app.MapHub<ContractStatsHub>("/hub/rest/contractstats");
app.MapHub<OrderBookHub>("/hub/rest/orderbook");
app.MapHub<LiqOrdersHub>("/hub/rest/liqorders");
#endregion

#region Strategies hub
app.MapHub<BreakoutStrategyHub>("/hub/strategy/breakoutstrategy");
#endregion
#endregion


app.UseCors(MyAllowSpecificOrigins);
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
