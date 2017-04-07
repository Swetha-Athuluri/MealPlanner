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
        Recipe GetRecipe(int recipeId,int userId);
        void SaveRecipe(Recipe recipe);
        List<Recipe> GetUsersRecipes(int userId);
        bool ModifyRecipe(Recipe recipe);
        List<Recipe> GetTop10RecentlyAddedRecipes();

    }
}