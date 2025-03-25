using TradeHorizon.Business.Interfaces;
using TradeHorizon.Business.Services;
using TradeHorizon.DataAccess.Interfaces;
using TradeHorizon.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient(); 
builder.Services.AddScoped<ICoinalyzeRepository, CoinalyzeRepository>();
builder.Services.AddScoped<ICoinalyzeService, CoinalyzeService>();
var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
