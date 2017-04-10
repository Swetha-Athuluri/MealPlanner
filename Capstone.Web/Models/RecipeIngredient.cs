using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class RecipeIngredient
    {
        public int Recipe_id { get; set; }
        public string Ingredient_Name { get; set; }
        //public int Quantity { get; set; }
        public string Quantity { get; set; }
        public string Measurement { get; set; }
        public static List<string> ingredientList { get; set; } 
    }
}