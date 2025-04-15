using TradeHorizon.Business.Interfaces.RestAPI;
using TradeHorizon.Business.Services.RestAPI;
using TradeHorizon.DataAccess.Interfaces.RestAPI;
using TradeHorizon.DataAccess.Repositories.RestAPI;
using TradeHorizon.API.Hubs;
using TradeHorizon.DataAccess.Repositories.Websocket;
using TradeHorizon.Domain.Websockets.Interfaces;
using TradeHorizon.Business.Services.Websocket;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSignalR();

#region DI container REST
builder.Services.AddScoped<ICoinalyzeRepository, CoinalyzeRepository>();
builder.Services.AddScoped<ICoinalyzeService, CoinalyzeService>();
builder.Services.AddScoped<IGateioRepository, GateioRepository>();
builder.Services.AddScoped<IGateioService, GateioService>();
#endregion

#region DI container WS - Gate Ticker
builder.Services.AddSingleton<IGateTickerBroadcaster, GateTickerBroadcaster>();  // Broadcaster
builder.Services.AddSingleton<IGateTickerProcessor, GateTickerService>(); 

#region DI container WS - Gate Trades
builder.Services.AddSingleton<IGateTradesBroadcaster, GateTradeBroadcaster>();  // Broadcaster
builder.Services.AddSingleton<IGateTradesProcessor, GateTradesService>();

builder.Services.AddSingleton<IGateTickerClient>(provider =>
    new GateTickerClient(
        provider.GetRequiredService<IGateTickerProcessor>(),
        new GateWebSocketClient(
            provider.GetRequiredService<IGateTickerProcessor>(),
            "wss://fx-ws.gateio.ws/v4/ws/usdt"
        ),
        "wss://fx-ws.gateio.ws/v4/ws/usdt"
    ));
#endregion

builder.Services.AddSingleton<IGateTradesClient>(provider =>
    new GateTradeClient(
        provider.GetRequiredService<IGateTradesProcessor>(),
        new GateWebSocketClient(
            provider.GetRequiredService<IGateTradesProcessor>(),
            "wss://fx-ws.gateio.ws/v4/ws/usdt"
        ),
        "wss://fx-ws.gateio.ws/v4/ws/usdt"
    ));

#endregion

#region CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:8000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});
#endregion

var app = builder.Build();

app.MapHub<GateTickerHub>("/ws/gate-ticker");
app.MapHub<GateTradesHub>("/ws/gate-trades");

app.UseCors(MyAllowSpecificOrigins);
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
