using TradeHorizon.Application.Interfaces;
using TradeHorizon.Application.Services;
using TradeHorizon.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddHttpClient();  // Add HTTP client for API calls
builder.Services.AddScoped<ICoinalyzeRepository, CoinalyzeRepository>();
builder.Services.AddScoped<ITradeService, TradeService>();
var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
