using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Capstone.Web.Models.ViewModels
{
    public class MealRecipeViewModel
    {
        public int UserId { get; set; }
        public int RecipeId { get; set;}
        public string MealType { get; set; }

        public static List<SelectListItem> MealTypes { get; } = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Entree" , Value="Entree"},
            new SelectListItem() {Text="Sidedish" , Value="Sidedish"},
            new SelectListItem() {Text="Snack" , Value="Snack"},
            new SelectListItem() {Text="Appetizer" , Value="Appetizer"},
            new SelectListItem() {Text="Dessert" , Value="Dessert"},

        };

        public int MealId { get; set; }
        public string MealName { get; set; }
        public string RecipeName { get; set; }
        public static List<Recipe> ListOfRecipies { get; set; }
        public static List<SelectListItem> RecipeList = ListOfRecipies.ConvertAll(ListOfRecipies =>
        {
            return new SelectListItem()
            {
                Text = ListOfRecipies.Name,
                Value = ListOfRecipies.RecipeId.ToString(),
                Selected = false
            };
        });



    }
}