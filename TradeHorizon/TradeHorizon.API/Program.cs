using TradeHorizon.Business.Interfaces;
using TradeHorizon.Business.Services;
using TradeHorizon.DataAccess.Interfaces;
using TradeHorizon.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient(); 

// register the dependency injection (DI) container.
#region  DI container
builder.Services.AddScoped<ICoinalyzeRepository, CoinalyzeRepository>();
builder.Services.AddScoped<ICoinalyzeService, CoinalyzeService>();
builder.Services.AddScoped<IGateioRepository, GateioRepository>();
builder.Services.AddScoped<IGateioService, GateioService>();
#endregion

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
