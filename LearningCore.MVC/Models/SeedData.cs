using LearningCore.Common.Extentions;
using LearningCore.Common.Helpers;
using LearningCore.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LearningCore.MVC.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LearningCoreContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<LearningCoreContext>>()))
            {
                var jsondata = FileHelper.ReadFile($"{AppContext.BaseDirectory}Files\\InitializeData.json");

                if (context.MxAttributes.Any())
                {
                    return;   // DB has been seeded
                }
                if (jsondata.IsNullOrWhiteSpace())
                    return;
                var result = JsonSerializer.Deserialize<InitializeData_Json>(jsondata);//, typeof(InitializeData_Json)
                context.MxAttributes.AddRange(
                  result.mx_Attributes
                );
                context.Movie.AddRange(
                    result.movies
                    );
                context.SaveChanges();
            }
        }
    }
}
