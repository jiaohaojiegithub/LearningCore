using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LearningCore.MVC.Models;
using LearningCore.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using LearningCore.Common.Helpers;
using System.IO;
using LearningCore.Common.Extentions;
using System.Dynamic;
using System.Text;
using System.Web;
using System.Text.Unicode;

namespace LearningCore.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            #region
            var data = new List<Mx_Attribute>
            {
                 new Mx_Attribute { AttributeName = "男",AttributeType="性别",AttributeValue="男",Description="性别",CreatedTime=DateTime.Now },
                  new Mx_Attribute { AttributeName = "女",AttributeType="性别",AttributeValue="女",Description="性别",CreatedTime=DateTime.Now }
            };
            var moviesdata = new List<Movie> {
             new Movie
                    {
                        Title = "When Harry Met Sally",
                        ModifiedTime = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ModifiedTime = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },

                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ModifiedTime = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },

                    new Movie
                    {
                        Title = "Rio Bravo",
                        ModifiedTime = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M                      

                        
                    }
            };
            #endregion
            string utf8result = Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(data));
            var result = JsonSerializer.Serialize(data,new JsonSerializerOptions { Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All) });//序列化中文乱码问题
            var basePath = string.Empty;
            basePath = Directory.GetParent(AppContext.BaseDirectory).FullName;            
             var jsondata = FileHelper.ReadFile($"{AppContext.BaseDirectory}Files\\InitializeData.json");
                //FileConfigHelper.GetJsonConfig("InitializeData.json", $"{AppContext.BaseDirectory}Files\\").GetSection("MX_Attribute");            
            var s = JsonSerializer.Deserialize<InitializeData_Json>(jsondata);//, typeof(InitializeData_Json)
          
            //var serializerOptions = new JsonSerializerOptions
            //{
            //    Converters = { new DynamicJsonConverter() }
            //};
            //var dynamicS = JsonSerializer.Deserialize<dynamic>("{\"name\":\"test\"}", serializerOptions);
            //Console.WriteLine(dynamicS.name);
            dynamic p = JsonSerializer.Deserialize("{\"name\":\"test\"}", typeof(ExpandoObject));//序列化成动态类型
            Console.WriteLine(p.name);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
