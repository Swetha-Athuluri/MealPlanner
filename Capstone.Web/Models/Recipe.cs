using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class Recipe
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
        public string ImageName { get; set; }
        public string RecipeType { get; set; }
        public int CookTimeInMinutes { get; set; }
    }
}
        
        
      

     

