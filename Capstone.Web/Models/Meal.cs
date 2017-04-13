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
        public List<string> RecipeNames { get; set; }
        public string MealImageName { get; set; }
        public List<MealRecipe> Recipes { get; set; } = new List<MealRecipe>(); //Read Only DO NOT USE FOR ADD AND MODIFY


    }
    public class MealRecipe
    {
        public string RecipeName { get; set; }
        public string MealType { get; set; }
    }
}