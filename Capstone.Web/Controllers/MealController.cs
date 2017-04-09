using Capstone.Web.DAL;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class MealController : Controller
    {
        private IRecipeDAL recipeDAL;
        private IUserDAL userDAL;
        private IMealDAL mealDAL;

        public MealController(IRecipeDAL recipeDAL, IUserDAL userDAL, IMealDAL mealDAL)
        {
            this.recipeDAL = recipeDAL;
            this.userDAL = userDAL;
            this.mealDAL = mealDAL;
        }
        [HttpGet]
        public ActionResult CreateMeal()
        {
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View("CreateMeal", recipeDAL.GetUsersRecipes((int)Session[SessionKeys.UserId]));
        }

        [HttpPost]
        public ActionResult CreateMeal(MealRecipeViewModel model)
        {
            if (model != null && model.RecipeId != null && model.MealType != null)
            {
                model.UserId = (int)Session[SessionKeys.UserId];
                mealDAL.SaveMeal(model);
                //model.ListOfRecipies 
                //foreach (var recipe in model.ListOfRecipies)
                //{
                //   // mealDAL.SaveMeal()
                //}

                return View("SuccessfullyAddedRecipe", model);
            }
            return RedirectToAction("Login", "User");
        }

        // GET: Meal
        public ActionResult Index()
        {
            return View();
        }
    }
}