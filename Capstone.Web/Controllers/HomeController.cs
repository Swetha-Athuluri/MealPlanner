﻿using Capstone.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models.ViewModels;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRecipeDAL recipeDAL;
        private IUserDAL userDAL;
        private IRecipeIngredientDAL recipeIngredientDAL;
        private IIngredientDAL ingredientDAL;
        private IPreparationStepsDAL preparationStepsDAL;

        public HomeController(IRecipeDAL recipeDAL, IUserDAL userDal, IRecipeIngredientDAL recipeIngredientDAL, IIngredientDAL ingredientDAL, IPreparationStepsDAL preparationStepsDAL)
        {
            this.recipeDAL = recipeDAL;
            this.userDAL = userDal;
            this.recipeIngredientDAL = recipeIngredientDAL;
            this.ingredientDAL = ingredientDAL;
            this.preparationStepsDAL = preparationStepsDAL;
        }

        // GET: Home
        public ActionResult Index()
        {

            // return View("Index");

            List<RecipeViewModel> listOfRecipesView = new List<RecipeViewModel>();
            if (Session[SessionKeys.EmailAddress] != null)
            {
                List<Recipe> recipes = recipeDAL.GetTop10RecentlyAddedUserRecipes((int)Session[SessionKeys.UserId]);
                
                GetTop10RecipeDetails(listOfRecipesView, recipes);

            }
            else
            {
                List<Recipe> recipes = recipeDAL.GetTop10RecentlyAddedRecipes();
                GetTop10RecipeDetails(listOfRecipesView, recipes);
            }

            return View("Index", listOfRecipesView);

        }

        private void GetTop10RecipeDetails(List<RecipeViewModel> listOfRecipesView, List<Recipe> recipes)
        {
            foreach (Recipe recipe in recipes)
            {
                List<RecipeIngredient> recipeIngredients = recipeIngredientDAL.GetRecipeIngredients(recipe.RecipeId);
                List<PreparationSteps> steps = preparationStepsDAL.GetPreparationStepsForRecipe(recipe.RecipeId);
                RecipeViewModel rvm = new RecipeViewModel();
                rvm.RecipeName = recipe.Name;
                rvm.RecipeId = recipe.RecipeId;
                rvm.RecipeCookTimeInMinutes = recipe.CookTimeInMinutes;
                rvm.RecipeImageName = recipe.ImageName;

                rvm.RecipeIngredient = recipeIngredients;
                //rvm.PrepSteps = new List<string>();
                rvm.PrepSteps = new List<string>();
                if (steps != null)
                {
                    foreach (var step in steps)
                    {
                        rvm.PrepSteps.Add(step.Steps);
                    }
                }
                listOfRecipesView.Add(rvm);
            }
        }

        [ChildActionOnly]
        public ActionResult UploadPhoto()
        {
            if (Session[SessionKeys.EmailAddress] != null)
            {
                return PartialView("_UploadPhoto");
            }
            return RedirectToAction("Login", "User");
        }
    }
}