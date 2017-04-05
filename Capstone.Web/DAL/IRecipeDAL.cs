using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.DAL
{
    public interface IRecipeDAL
    {
        List<Recipe> GetAllRecipes();
        Recipe GetRecipe(int recipeId);
        //void SaveRecipe(Recipe newRecipe, List<string> steps);
        void SaveRecipe(RecipeViewModel rvm);
        List<Recipe> GetUsersRecipes(User userName);

    }
}