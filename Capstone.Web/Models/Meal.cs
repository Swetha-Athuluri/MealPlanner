using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Models
{
    public class Meal
    {
        List<Recipe> Recipes { get; set; }
        string MealName { get; set; }
        SelectListItem MealTypes { get; }

    }
}