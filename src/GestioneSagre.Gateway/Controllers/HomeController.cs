using GestioneSagre.Gateway.Controllers.Common;

namespace GestioneSagre.Gateway.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> logger;

    public HomeController(ILogger<HomeController> logger)
    {
        this.logger = logger;
    }

    [HttpGet]
    public IActionResult Welcome()
    {
        var todayDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time"));

        logger.LogInformation($"Hello World at {todayDateTime:HH:mm:ss} hours of day {todayDateTime:dd/MM/yyyy} !");
        return Ok($"Hello World at {todayDateTime:HH:mm:ss} hours of day {todayDateTime:dd/MM/yyyy} !");
    }
}