using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;

namespace Capstone.Web.DAL
{
    public interface IMealDAL
    {
        List<Recipe> GetAllMeals(int userId);
        void SaveMeal(Meal meal, int userId);
        void DeleteMealRecipe(int userId, int recipeId);
        // void SaveMealRecipe(MealRecipeViewModel mealRecipeViewModel);
        

    }
}
