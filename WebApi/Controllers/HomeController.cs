using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("/")]
	[ApiController]
    public class HomeController : Controller
    {
	    private readonly ILogger<HomeController> _logger;
	    private string RunningMessage() => Worker.ApiCommand ?? "Working service running....";

		public HomeController(ILogger<HomeController> logger)
	    {
			_logger = logger;
		}

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            _logger.LogInformation(RunningMessage());
            await Task.Delay(10);

            return Ok(RunningMessage());
        }

    }
}
