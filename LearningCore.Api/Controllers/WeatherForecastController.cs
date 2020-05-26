using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LearningCore.Data;
using LearningCore.Data.MVCModels;
using LearningCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LearningCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IOptions<WxConfigModel> _wxConfig;
        private readonly IFilesService _filesService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , IConfiguration configuration
            , IOptions<WxConfigModel> wxConfig
            , IFilesService filesService)
        {
            _logger = logger;
            _configuration = configuration;
            _wxConfig = wxConfig;
            _filesService = filesService;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("{city}")]
        public string Get(string city)
        {
            if (!string.Equals(city?.TrimEnd(), "Redmond", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException(
                    $"We don't offer a weather forecast for {city}.", nameof(city));
            }

            return city;
        }

        [HttpGet("GetConfigurationStr/{Key}")]
        public string GetConfigurationStr(string Key= "WxConfig:AppId")
        {
            return _configuration.GetSection(Key).Value;
        }
        [HttpGet("GetWxConfig")]
        public string GetWxConfig()
        {
            return _wxConfig.Value.AppSecret;
        }
        [HttpGet("GetFilesList")]
        public Task<IEnumerable<AppFile>> GetFilesList()
        {           
            return _filesService.GetList();
        }
        /// <summary>
        /// 顶级节点验证
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        //[AcceptVerbs("GET", "POST")]
        [HttpGet("VerifyPhone/{phone}")]
        public IActionResult VerifyPhone(
    [RegularExpression(@"^\d{3}-\d{3}-\d{4}$")] string phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest($"Phone {phone} has an invalid format. Format: ###-###-####");
            }

            return Ok(true);
        }
    }
}
