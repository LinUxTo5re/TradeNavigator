using TradeHorizon.Business.Interfaces.RestAPI;
using TradeHorizon.Business.Services.RestAPI;
using TradeHorizon.DataAccess.Interfaces.RestAPI;
using TradeHorizon.DataAccess.Repositories.RestAPI;
using TradeHorizon.API.Hubs;
using TradeHorizon.DataAccess.Repositories.Websocket;
using TradeHorizon.Domain.Websockets.Interfaces;
using TradeHorizon.API.Hubs.Broadcaster;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient(); 
builder.Services.AddSignalR();

// register the dependency injection (DI) container.
#region  DI container REST
builder.Services.AddScoped<ICoinalyzeRepository, CoinalyzeRepository>();
builder.Services.AddScoped<ICoinalyzeService, CoinalyzeService>();
builder.Services.AddScoped<IGateioRepository, GateioRepository>();
builder.Services.AddScoped<IGateioService, GateioService>();
#endregion

#region DI container WS
// WebSocket Broadcaster (API layer)
builder.Services.AddSingleton<ISignalRBroadcaster, SignalRBroadcaster<GateTickerHub>>();
builder.Services.AddSingleton<IGateTickerProcessor, GateTickerService>();
builder.Services.AddSingleton<IGateTickerClient, GateioTickerClient>();
#endregion

#region start third party ws on start-up
//builder.Services.AddHostedService<GateioTickerClient>();
#endregion

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder
                .WithOrigins("http://localhost:8000") // <-- match the origin of your HTML page
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials(); // <-- needed if SignalR is sending cookies/etc.
        });
});
var app = builder.Build();

app.MapHub<GateTickerHub>("/ws/gate-ticker");
app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
