using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearningCore.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using Microsoft.Extensions.Options;
using LearningCore.Data;
using Microsoft.Extensions.Configuration;

namespace LearningCore.Api.Controllers.Tests
{
    [TestClass()]
    public class WeatherForecastControllerTests
    {
        //private readonly ILogger<WeatherForecastController> _logger;
        //private readonly IConfiguration _configuration;
        //private readonly IOptions<WxConfigModel> _wxConfig;
        //public WeatherForecastControllerTests(ILogger<WeatherForecastController> logger
        //    , IConfiguration configuration
        //    , IOptions<WxConfigModel> wxConfig)
        //{
        //    _logger = logger;
        //    _configuration = configuration;
        //    _wxConfig = wxConfig;
        //}

        [TestMethod()]
        public void GetTest()
        {
            // Arrange 初始准备
            var logger = new Mock<ILogger<WeatherForecastController>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockWxConfigOptions = new Mock<IOptions<WxConfigModel>>();
            mockWxConfigOptions.Setup(_ => _.Value).Returns(new WxConfigModel
            {
                AppId = "aaa",
                AppSecret = "bbb"
            });
            var controller = new WeatherForecastController(logger.Object, mockConfiguration.Object, mockWxConfigOptions.Object);

            // Act 行为
            var result = controller.Get();

            // Assert 断言

            Assert.AreEqual(5, result.Count());

        }

        [TestMethod()]
        public void GetConfigurationStrTest()
        {

            // Arrange 初始准备
            var logger = new Mock<ILogger<WeatherForecastController>>();
            var mockConfiguration = new Mock<IConfiguration>();
            //mockConfiguration.SetupGet(_ => _.GetSection("WxConfig").Value).Returns("{\"AppId\": \"aaa\",\"AppSecret\": \"bbb\" }"); 
            mockConfiguration.SetupGet(_ => _.GetSection("WxConfig:AppId").Value).Returns("aaa");
            mockConfiguration.SetupGet(_ => _.GetSection("WxConfig:AppSecret").Value).Returns("bbb");
            var mockWxConfigOptions = new Mock<IOptions<WxConfigModel>>();
            mockWxConfigOptions.Setup(_ => _.Value).Returns(new WxConfigModel
            {
                AppId = "aaa",
                AppSecret = "bbb"
            });
            var controller = new WeatherForecastController(logger.Object, mockConfiguration.Object, mockWxConfigOptions.Object);
            // Act 行为
            var result = controller.GetConfigurationStr("WxConfig:AppSecret");

            // Assert 断言

            Assert.AreEqual("bbb", result);
        }

        [TestMethod()]
        public void GetWxConfigTest()
        {
            // Arrange 初始准备
            var logger = new Mock<ILogger<WeatherForecastController>>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockWxConfigOptions = new Mock<IOptions<WxConfigModel>>();
            mockWxConfigOptions.Setup(_ => _.Value).Returns(new WxConfigModel
            {
                AppId = "aaa",
                AppSecret = "bbb"
            });
            var controller = new WeatherForecastController(logger.Object, mockConfiguration.Object, mockWxConfigOptions.Object);
            // Act 行为
            var result = controller.GetWxConfig();

            // Assert 断言

            Assert.AreEqual("bbb", result);
        }
    }
}