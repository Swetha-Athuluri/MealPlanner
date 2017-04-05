using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class PreparationSteps
    {
        public int RecipeId { get; set; }
        public int StepId { get; set; }
        public string Steps { get; set; }
    }
}