namespace GestioneSagre.Gateway.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class HomeController : ControllerBase
{
    private readonly ILoggerService logger;

    public HomeController(ILoggerService logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    public IActionResult Welcome()
    {
        var todayDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));

        logger.SaveLogInformation($"Hello World at {todayDateTime:HH:mm:ss} hours of day {todayDateTime:dd/MM/yyyy} !");
        return Ok($"Hello World at {todayDateTime:HH:mm:ss} hours of day {todayDateTime:dd/MM/yyyy} !");
    }
}