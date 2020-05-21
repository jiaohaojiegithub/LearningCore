using LearningCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningCore.MVC.Models
{
    public class InitializeData_Json
    {
        public List<Mx_Attribute> mx_Attributes { get; set; }
        public List<Movie> movies { get; set; }
    }
}
