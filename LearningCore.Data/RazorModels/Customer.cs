using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LearningCore.Data.RazorModels
{
    public class Customer:EntityBase<long>
    {     
        [Display(Name ="名"), Description("名")]
        [Required, StringLength(10)]
        public string Name { get; set; }
        [EmailAddress,NotMapped]
        public string Email { get; set; }
    }
}
