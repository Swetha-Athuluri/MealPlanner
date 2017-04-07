﻿using Capstone.Web.DAL;
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
        private IRecipeIngredientDAL recipeIngredientDAL;
        private IIngredientDAL ingredientDAL;
        private IPreparationStepsDAL preparationStepsDAL;

        public RecipeController(IRecipeDAL recipeDAL, IUserDAL userDal, IRecipeIngredientDAL recipeIngredientDAL, IIngredientDAL ingredientDAL, IPreparationStepsDAL preparationStepsDAL)
        {
            this.recipeDAL = recipeDAL;
            this.userDAL = userDal;
            this.recipeIngredientDAL = recipeIngredientDAL;
            this.ingredientDAL = ingredientDAL;
            this.preparationStepsDAL = preparationStepsDAL;
        }
        // GET: Recipe
        public ActionResult Index()
        {
            return View("Recipes");
        }
        public ActionResult Detail(int recipeId)
        {
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login", "User");
            }

            //  return View("Recipes", recipeDAL.GetUsersRecipes((int)Session[SessionKeys.UserId]));
            Recipe r = recipeDAL.GetRecipe(recipeId, (int)Session[SessionKeys.UserId]);
            List<RecipeIngredient> recipeIngredients = recipeIngredientDAL.GetRecipeIngredients(recipeId);
            List<PreparationSteps> steps = preparationStepsDAL.GetPreparationStepsForRecipe(recipeId);

            RecipeViewModel rvm = new RecipeViewModel();
            rvm.RecipeName = r.Name;
            rvm.RecipeId = r.RecipeId;
            rvm.RecipeCookTimeInMinutes = r.CookTimeInMinutes;

            rvm.RecipeIngredient = recipeIngredients;
            rvm.Steps = steps;

            return View("Detail", rvm);
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

            if (model != null && model.QuantityMeasurementIngredient != null && model.PrepSteps != null)
            {

                List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
                PreparationSteps pS = new PreparationSteps();
                List<string> prepSteps = new List<string>();

                foreach (var item in model.QuantityMeasurementIngredient)
                {
                    var QMIP = item.Split(',');
                    if (QMIP[0] != "")
                    {
                        RecipeIngredient recipeIngredient = new RecipeIngredient()
                        {
                            Quantity = Convert.ToInt32(QMIP[0]),
                            Measurement = QMIP[1],
                            IngredientName = QMIP[2],
                        };

                        recipeIngredients.Add(recipeIngredient);
                    }
                }
                foreach (var step in model.PrepSteps)
                {
                    prepSteps.Add(step);
                }

                Recipe r = new Recipe();
                r.Name = model.RecipeName;
                r.Description = model.RecipeDescription;
                r.ImageName = model.RecipeName;
                r.CookTimeInMinutes = model.RecipeCookTimeInMinutes;
                r.RecipeType = model.RecipeType;

                if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) != null)
                {
                    model.UserId = (int)Session[SessionKeys.UserId];
                    recipeDAL.SaveRecipe(model);
                    recipeIngredientDAL.SaveRecipeIngredients(recipeIngredients);
                    preparationStepsDAL.SavePreparationSteps(r.RecipeId, prepSteps, pS); //might need to get RECIPEID from DAL
                    return View("SuccessfullyAddedRecipe", model);

                }
            }
            return RedirectToAction("Login", "User");
        }

        // GET: All User Recipes
        public ActionResult Recipes()
        {
            // int userId = (int)Session[SessionKeys.UserId];
            if (Session[SessionKeys.UserId] == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View("Recipes", recipeDAL.GetUsersRecipes((int)Session[SessionKeys.UserId]));
        }
    }
}