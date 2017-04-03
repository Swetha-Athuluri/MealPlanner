using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeDAL recipeDAL; 

        public RecipeController (IRecipeDAL recipeDAL)
        {
            this.recipeDAL = recipeDAL; 
        }
        // GET: Recipe
        public ActionResult Index()
        {
            return View("Recipes"); 
        }
        public ActionResult Detail(int recipeId)
        {
            Recipe model = recipeDAL.GetRecipe(recipeId);

            return View("Detail", model); 
        }
    }
}