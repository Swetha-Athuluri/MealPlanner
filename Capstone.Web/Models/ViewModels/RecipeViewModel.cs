using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models.ViewModels
{
    public class RecipeViewModel
    {
        public int UserId { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeImageName { get; set; }
        public int RecipeId { get; set; }
        public List <string> QuantityMeasurementIngredient { get; set; }
        [Required]
        public List <string> PrepSteps { get; set; }

        [RegularExpression("^(?=.*[0-9].*[0-9].*[0-9])$")]

        [Required]
        public int RecipeCookTimeInMinutes { get; set; }

        [Required]
        public string IngredientName { get; set; }
       
        // public List<Ingredient> Ingredients { get; set; }
        //public int IngredientId { get; set; }

        public List<int> IngredientId { get; set; }
        public List<int> IngredientQuantity { get; set; }
        //public List<string> Ingredient_Name { get; set; }
        public List<string> IngredientMeasurementOptions { get; set; }

        public string MeasurementType { get; set; }


        //public string Measurement { get; set; }

       // public string Ingredient_Name { get; set; }
        //public string QuantityOfIngredients { get; set; }

        public List<RecipeIngredient> RecipeIngredient { get; set; }
       
        public string RecipeType { get; set; }
        public string Email { get; set; }
        public List<PreparationSteps> Steps { get; set; }
        public MultiSelectList RecipeSelectionTypes { get; set; }
        public static List<SelectListItem> RecipeTypes { get; } = new List<SelectListItem>()
        {
            new SelectListItem() {Text = "Vegan", Value = "Vegan" },
            new SelectListItem() {Text = "Vegetarian", Value = "Vegetarian" },
            new SelectListItem() {Text = "Kid Friendly", Value = "Kid Friendly" },
            new SelectListItem() {Text = "Meat Lover", Value = "Meat Lover" },
            new SelectListItem() {Text = "Cheesy Deliciousness", Value = "Cheesy Deliciousness" },
            new SelectListItem() {Text = "Under 30 minutes", Value = "Under 30 Minutes" },
            new SelectListItem() {Text = "Healthy", Value = "Healthy" }

        };

        public static List<SelectListItem> Measurements { get; } = new List<SelectListItem>()
        {
            new SelectListItem() {Text = "bunch(es)", Value = "bunch(es)" },
            new SelectListItem() {Text = "head(s)", Value = "head(s)" },
            new SelectListItem() {Text ="cup(s)", Value = "cup(s)" },
            new SelectListItem() {Text ="ounce(s)", Value = "ounce(s)" },
            new SelectListItem() {Text ="pound(s)", Value = "pound(s)" },
            new SelectListItem() {Text ="pint(s)",Value="pint(s)" },
            new SelectListItem() {Text ="quart(s)", Value="quart(s)" },
            new SelectListItem() {Text ="ml",Value="ml" },
            new SelectListItem() {Text ="teaspoon(s)",Value="teaspoon(s)" },
            new SelectListItem() {Text ="tablespoon(s)",Value="tablespoon(s)" },
            new SelectListItem() {Text ="gallon(s)",Value="gallon(s)" },
            new SelectListItem() {Text ="liter(s)",Value="liter(s)" },
            new SelectListItem() {Text ="inch(es)",Value="inch(es)" },
        };

       
    }
}
