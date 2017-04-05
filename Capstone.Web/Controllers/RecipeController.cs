using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models.ViewModels;

namespace Capstone.Web.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeDAL recipeDAL;
        private IUserDAL userDAL; 

        public RecipeController(IRecipeDAL recipeDAL, IUserDAL userDal)
        {
            this.recipeDAL = recipeDAL;
            this.userDAL = userDal; 
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

        [HttpGet]
        public ActionResult CreateRecipe()
        {
            return View("CreateRecipe"); 
        }

        [HttpPost]
        public ActionResult CreateRecipe(RecipeViewModel model)
        { 
            
            if (model != null)
            {
                Recipe r = new Recipe();
                r.Name = model.RecipeName;
                r.Description = model.RecipeDescription;
                r.ImageName = model.RecipeName;
                r.CookTimeInMinutes = model.RecipeCookTimeInMinutes;
                if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) != null)
                {
                User u = userDAL.GetUser((string)Session[SessionKeys.EmailAddress]);
                    r.UserId = u.Id;
                recipeDAL.SaveRecipe(r);
                }
                else
                {
                    return View("Login", "User"); 
                }
                
            }


            return View("SuccessfullyAddedRecipe", model);


        }
    }
}