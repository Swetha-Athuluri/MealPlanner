using Capstone.Web.DAL;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models.ViewModels;
using System.IO;

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
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) != null)
            {
                int userId = (int)Session[SessionKeys.UserId];
                List<Recipe> model = recipeDAL.GetUsersRecipes(userId);
                return View("Recipes", model);
            }
            return RedirectToAction("Login", "User");
        }
        [HttpGet]
        public ActionResult Detail(int recipeId)
        {
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) == null)
            {
                return RedirectToAction("Login", "User");
            }

            Recipe r = recipeDAL.GetRecipe(recipeId, (int)Session[SessionKeys.UserId]);
            List<RecipeIngredient> recipeIngredients = recipeIngredientDAL.GetRecipeIngredients(recipeId);
            List<PreparationSteps> steps = preparationStepsDAL.GetPreparationStepsForRecipe(recipeId);

            RecipeViewModel rvm = new RecipeViewModel();
            rvm.RecipeName = r.Name;
            rvm.RecipeId = r.RecipeId;
            rvm.RecipeCookTimeInMinutes = r.CookTimeInMinutes;
            rvm.RecipeImageName = r.ImageName; 

            rvm.RecipeIngredient = recipeIngredients;
            rvm.PrepSteps = new List<string>(); 
            if (steps != null)
            {
                foreach (var step in steps)
                {
                    rvm.PrepSteps.Add(step.Steps);
                }
            }



            return View("Detail", rvm);
        }

        [HttpGet]
        public ActionResult CreateRecipe()
        {
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View("CreateRecipe");
        }

        [HttpPost]
        public ActionResult CreateRecipe(RecipeViewModel model, HttpPostedFileBase recipeImage)
        {

            if (model != null && model.QuantityMeasurementIngredient != null && model.PrepSteps != null)
            {
                if (recipeImage.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(recipeImage.FileName);
                    var fullPath = Path.Combine(Server.MapPath("~/Recipe-Images"), fileName);
                    recipeImage.SaveAs(fullPath);
                    model.RecipeImageName = fileName;
                }
                else
                {
                    model.RecipeImageName = "stock.jpg";
                }


                List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
                PreparationSteps pS = new PreparationSteps();
                model.PrepSteps[0].Replace('\n', ' ');
                List<string> prepSteps = model.PrepSteps[0].Split('\r').ToList();

                foreach (var item in model.QuantityMeasurementIngredient)
                {

                    if (item != "")
                    {
                        RecipeIngredient recipeIngredient = new RecipeIngredient()
                        {
                            Quantity = "",
                            Measurement = "",
                            Ingredient_Name = item,
                        };

                        recipeIngredients.Add(recipeIngredient);
                    }
                }
                string ingredient1 = recipeIngredients[0].Ingredient_Name;

                Recipe r = new Recipe();
                r.Name = model.RecipeName;
                r.Description = model.RecipeDescription;
                r.ImageName = model.RecipeImageName;
                r.CookTimeInMinutes = model.RecipeCookTimeInMinutes;
                var recipeType = "";
                int counter = 0;
                foreach (var item in model.RecipeType)
                {
                    if (model.RecipeType.Count == counter)
                    {
                        recipeType += item;
                    }
                    else
                    {
                        recipeType += item + ", ";
                        counter++;
                    }
                }
                r.RecipeType = recipeType;

                if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) != null)
                {
                    r.UserId = (int)Session[SessionKeys.UserId];
                    recipeDAL.SaveRecipe(r);

                    recipeIngredientDAL.SaveRecipeIngredients(recipeIngredients, r.RecipeId);
                    preparationStepsDAL.SavePreparationSteps(r.RecipeId, prepSteps, pS); //might need to get RECIPEID from DAL
                    TempData["action"] = "save";
                    return RedirectToAction("Detail", new { recipeId = r.RecipeId });

                }
            }
            return RedirectToAction("CreateRecipe", "Recipe");
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

        [HttpGet]
        public ActionResult ModifyRecipeView(int recipeId)
        {
            if (userDAL.GetUser((string)Session[SessionKeys.EmailAddress]) == null)
            {
                return RedirectToAction("Login", "User");
            }
            List<RecipeIngredient> recipeIngredients = recipeIngredientDAL.GetRecipeIngredients(recipeId);
            Recipe recipe = recipeDAL.GetRecipe(recipeId, (int)Session[SessionKeys.UserId]);
            List<PreparationSteps> steps = preparationStepsDAL.GetPreparationStepsForRecipe(recipeId);
            string[] arrayOfStrings = recipe.RecipeType.Split(',');
            //Recipe r = recipeDAL.ModifyRecipe(recipeId, (int)Session[SessionKeys.UserId]);
            RecipeViewModel rvm = new RecipeViewModel();

            rvm.RecipeType = arrayOfStrings.ToList<string>();
            rvm.RecipeImageName = recipe.ImageName;
            rvm.RecipeDescription = recipe.Description;
            rvm.RecipeCookTimeInMinutes = recipe.CookTimeInMinutes;
            rvm.RecipeIngredient = recipeIngredients;
            rvm.RecipeName = recipe.Name; 

            rvm.PrepSteps = new List<string>();
            if (steps != null)
                
            {
                foreach(var step in steps)
                {
                    rvm.PrepSteps.Add(step.Steps);
                }
            }

            
                 return View("ModifyRecipeView", rvm); 
        }
    }
}