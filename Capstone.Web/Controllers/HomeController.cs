using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {

            return View("Index");

            List<RecipeViewModel> listOfRecipesView = new List<RecipeViewModel>();
            if (Session[SessionKeys.EmailAddress] != null)
            {
                List<Recipe> recipes = recipeDAL.GetTop10RecentlyAddedUserRecipes((int)Session[SessionKeys.UserId]);
                //List<RecipeViewModel> listOfRecipesView = new List<RecipeViewModel>();
                foreach (Recipe recipe in recipes)
                {
                    List<RecipeIngredient> recipeIngredients = recipeIngredientDAL.GetRecipeIngredients(recipe.RecipeId);
                    List<PreparationSteps> steps = preparationStepsDAL.GetPreparationStepsForRecipe(recipe.RecipeId);
                    RecipeViewModel rvm = new RecipeViewModel();
                    rvm.RecipeName = recipe.Name;
                    rvm.RecipeId = recipe.RecipeId;
                    rvm.RecipeCookTimeInMinutes = recipe.CookTimeInMinutes;

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
            else {
                List<Recipe> recipes = recipeDAL.GetTop10RecentlyAddedRecipes();
                
                foreach (Recipe recipe in recipes)
                {
                    List<RecipeIngredient> recipeIngredients = recipeIngredientDAL.GetRecipeIngredients(recipe.RecipeId);
                    List<PreparationSteps> steps = preparationStepsDAL.GetPreparationStepsForRecipe(recipe.RecipeId);
                    RecipeViewModel rvm = new RecipeViewModel();
                    rvm.RecipeName = recipe.Name;
                    rvm.RecipeId = recipe.RecipeId;
                    rvm.RecipeCookTimeInMinutes = recipe.CookTimeInMinutes;

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

            return View("Index",listOfRecipesView);

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