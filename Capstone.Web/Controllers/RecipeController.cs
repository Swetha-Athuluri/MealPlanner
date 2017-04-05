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
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) == null)
            {
                // model.UserId = (int)Session[SessionKeys.UserId];
                //recipeDAL.SaveRecipe(r,m.steps);
                return RedirectToAction("Login", "User");
            }
            else
            {
                return View("CreateRecipe");
            }
            // return View("CreateRecipe"); 
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
                r.RecipeType = model.RecipeType;
                if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) != null)
                {
                    model.UserId = (int)Session[SessionKeys.UserId];
                    //recipeDAL.SaveRecipe(r,m.steps);
                    recipeDAL.SaveRecipe(model);
                }
                else
                {
                    return View("Login", "User");
                }

            }


            return View("SuccessfullyAddedRecipe", model);


        }

        // GET: All User Recipes
        public ActionResult UserRecipes()
        {
            int userId = (int)Session[SessionKeys.UserId];
            if(userId == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View("UserRecipes",recipeDAL.GetUsersRecipes(userId));
        }
    }
}