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
        void UpdateRecipe(Recipe recipe);
        List<Recipe> GetTop10RecentlyAddedRecipes();
        List<Recipe> GetTop10RecentlyAddedUserRecipes(int userId);
        List<string> GetRecipeNames(int userId);
<<<<<<< HEAD

=======
        void DeleteRecipe(Recipe recipe);
>>>>>>> 257b0701f802666ca5631279183ddb32c7c29c00
    }
}