using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Meal
    {
        public List<int> RecipeIds { get; set; }
        public string MealName { get; set; }
        public List<string> MealTypes { get; set; }
        public int MealId { get; set; }

    }
}