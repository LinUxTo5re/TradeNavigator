using TradeHorizon.Business.Interfaces.RestAPI;
using Polly;

public class FilterContractWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public FilterContractWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromMinutes(15));

        try
        {
            await ExecuteDataLoadAsync();

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                try
                {
                    await ExecuteDataLoadAsync();
                }
                catch (Exception)
                {
                    // TODO: Add logging for the error
                }
            }
        }
        catch (OperationCanceledException)
        {
            // TODO: Add logging for cancellation
        }
    }

    private async Task ExecuteDataLoadAsync()
    {
        using var scope = _scopeFactory.CreateScope();
        var filterContractService = scope.ServiceProvider.GetRequiredService<IFilterContractService>();

        // Define retry policy using Polly
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(5, attempt)), // 5^1=5s, 5^2=15s, 5^3=125s, etc.
                onRetry: (exception, timeSpan, attempt, context) =>
                {
                    // TODO: Log retry attempts, exception details, etc.
                });

        await retryPolicy.ExecuteAsync(async () =>
        {
            var data = await filterContractService.GetFilteredContractsList();
        });
    }
}
