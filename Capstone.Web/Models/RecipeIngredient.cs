using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class RecipeIngredient
    {
        public string IngredientName { get; set; }
        public int Quantity { get; set; }
        public string Measurement { get; set; }
    }
}