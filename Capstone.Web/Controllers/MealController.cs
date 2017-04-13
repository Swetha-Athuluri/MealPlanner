using Capstone.Web.DAL;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;

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

        public ActionResult UnderConstruction()
        {
            return View ("UnderConstruction");
        }

        [HttpGet]
        public ActionResult CreateMeal()
        {
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) == null)
            {
                return RedirectToAction("Login", "User");
            }
            List<Recipe> r = recipeDAL.GetUsersRecipes((int)Session[SessionKeys.UserId]);
            MealRecipeViewModel mRVM = new MealRecipeViewModel();


            foreach (var recipe in r)
            {
                mRVM.RecipeList.Add(new SelectListItem { Text = recipe.Name, Value = Convert.ToString(recipe.RecipeId) });

            }

            return View("CreateMeal", mRVM);
        }

        [HttpPost]
        public ActionResult CreateMeal(MealRecipeViewModel model)
        {
            if (model != null && model.MealType != null)
            {
                int userId = (int)Session[SessionKeys.UserId];
                List<int> recipeIds = new List<int>();
                List<string> mealTypes = new List<string>();
               
                foreach (var mt in model.MealType)
                {
                    //List<string> recipeMeal = recipeMealType.Split(',').ToList();
                    //recipeIds.Add(Convert.ToInt32(recipeMeal[0]));
                    mealTypes.Add(mt);
                }

                Meal meal = new Meal()
                {
                    MealName = model.MealName,
                    RecipeIds = model.RecipeIds,
                    MealTypes = mealTypes,
                    RecipeNames = model.RecipeNames
                    
                };

                mealDAL.SaveMeal(meal, userId);
                if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) != null)
                {
                    return RedirectToAction("Detail", "Meal", new { mealId = meal.MealId });
                }

            }
            return RedirectToAction("Login", "User");
        }
        [HttpGet]
        public ActionResult Detail(int mealId)
        {
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) == null)
            {
                return RedirectToAction("Login", "User");
            }
            int userId = (int)Session[SessionKeys.UserId];
            Meal m = mealDAL.GetMeal(mealId, userId);
            List<Recipe> recipes = new List<Recipe>();
            List<string> recipeNames = new List<string>();
            foreach (var recipe in m.RecipeIds)
            {
                recipes.Add(recipeDAL.GetRecipe(recipe, userId));
            }
            for (int i = 0; i < recipes.Count; i++)
            {
                recipeNames.Add(recipes[i].Name);
            }

            MealRecipeViewModel mrvm = new MealRecipeViewModel();
            mrvm.MealName = m.MealName;
            mrvm.MealImageName = recipes[0].ImageName;
            mrvm.MealId = m.MealId;
            mrvm.MealType = m.MealTypes;
            mrvm.RecipeNames = recipeNames;
      
            return View("Detail", mrvm);
        }

        // GET: Meal
        public ActionResult Index()
        {
            List<Meal> modelList = new List<Meal>();
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) != null)
            {
                int userId = (int)Session[SessionKeys.UserId];
                modelList = mealDAL.GetAllMeals(userId);
             }
            return View("Meals", modelList);


        }




    }
}
