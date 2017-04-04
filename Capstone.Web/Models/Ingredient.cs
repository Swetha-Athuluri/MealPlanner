using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Ingredient
    {
        public string Name { get; set; }
        public int Id { get; set; }
       

        public bool AddIngredient (string name)
        {
            return false; 
        }
    }
}