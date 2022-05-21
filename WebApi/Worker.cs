namespace WebApi
{
    public class Worker :BackgroundService
    {
	    private readonly ILogger<Worker > _logger;

	    public static string ApiCommand { get; set; } = "no command";

	    public Worker (ILogger<Worker > logger)
	    {
		    _logger = logger;
	    }

	    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	    {
		    while (!stoppingToken.IsCancellationRequested)
		    {
			    _logger.LogInformation("Worker running at {time}, command:{string}",DateTimeOffset.Now, ApiCommand);
			    await Task.Delay(1100, stoppingToken);
		    }
	    }
    }
}
