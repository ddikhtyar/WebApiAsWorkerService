using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class CommandController : ControllerBase
    {
	    private readonly ILogger<CommandController> _logger;
	    private string RunningMessage() => $"apiCommand: {Worker.ApiCommand}";

		public CommandController(ILogger<CommandController> logger)
	    {
			_logger = logger;
		}


		[HttpGet("{apiCommand}")]
		public async Task<ActionResult<string>> SetComand(string apiCommand)
		{
			await Task.Delay(10);

			Worker.ApiCommand = apiCommand;
			_logger.LogInformation(RunningMessage());

			return Ok(RunningMessage());
		}

	}
}
