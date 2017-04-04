using Capstone.Web.Models;
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
        void SaveRecipe(Recipe newRecipe);
        List<Recipe> GetUsersRecipes(User userName);

    }
}