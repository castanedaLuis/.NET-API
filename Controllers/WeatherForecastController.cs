using Microsoft.AspNetCore.Mvc;

namespace NET.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    private static List<WeatherForecast> ListWeatherForecast = new List<WeatherForecast>();

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        if(ListWeatherForecast == null || !ListWeatherForecast.Any())
        {
                ListWeatherForecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    //Date = DateTime.Now.AddDays(index),
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
                .ToList();
        }
    }

   [HttpGet(Name = "GetWeatherForecast")]
    [Route("Get/weatherforecast")]
    [Route("Get/weatherforecast2")]
    [Route("[action]")] //para tomar el nombre del metodo
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Retornando la lista de weatherforecast");
        return ListWeatherForecast;
    }

    [Route("Post/weatherforecast")]
    [Route("Post/weatherforecast2")]
    [Route("[action]")] 
    [HttpPost]
    public IActionResult Post(WeatherForecast weatherForecast)
    {
        ListWeatherForecast.Add(weatherForecast);
        return Ok("Registro insertado con Exito");
    }

    [HttpDelete("{index}")]
    public IActionResult Delete(int index)
    {
        if(index >= ListWeatherForecast.Count())
        {
            return NotFound("No se encuenta el elemento");
        }
        
        ListWeatherForecast.RemoveAt(index);
        return Ok("Registro Eliminado");
    }
}
