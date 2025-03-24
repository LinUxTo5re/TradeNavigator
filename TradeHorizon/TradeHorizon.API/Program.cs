using TradeHorizon.Business.Interfaces;
using TradeHorizon.Business.Services;
using TradeHorizon.DataAccess.Interfaces;
using TradeHorizon.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();  // Add HTTP client for API calls
builder.Services.AddScoped<ICoinalyzeRepository, CoinalyzeRepository>();
builder.Services.AddScoped<ITradeService, TradeService>();
var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
