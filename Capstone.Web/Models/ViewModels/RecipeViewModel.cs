using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models.ViewModels
{
    public class ReciepeViewModel
    {

        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }

        public string RecipeImageName { get; set; }
        public string RecipeType { get; set; }
        public int RecipeCookTimeInMinutes { get; set; }
        public List<Ingredient> Ingredients { get; set; }

        public string IngredientName { get; set; }
        public string QuantityOfIngredients { get; set; }
    }
}