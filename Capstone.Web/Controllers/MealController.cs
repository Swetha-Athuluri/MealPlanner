﻿using Capstone.Web.DAL;
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
            if (model != null && model.RecipeId != 0 && model.MealType != null)
            {
                int userId = (int)Session[SessionKeys.UserId];
                List<int> recipeIds = new List<int>();
                List<string> mealTypes = new List<string>(); 
                foreach (var recipeMealType in model.RecipeIdMealType)
                {
                    List<string> recipeMeal = recipeMealType.Split(',').ToList();
                    recipeIds.Add(Convert.ToInt32(recipeMeal[0]));
                    mealTypes.Add(recipeMeal[1]);
                }

                Meal meal = new Meal()
                {
                    MealName = model.MealName,
                    RecipeIds = recipeIds,
                    MealTypes = mealTypes

                };

                mealDAL.SaveMeal(meal, userId);
                
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