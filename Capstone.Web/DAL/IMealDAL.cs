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
        List<Meal> GetAllMeals(int userId);
        void SaveMeal(Meal meal, int userId);
        void DeleteMealRecipe(int userId, int mealId);

        //Modify Recipes to meal
        void DeleteRecipesForMeal(int userId,int mealId);
        void UpdateMeal(int mealId, string mealName);
        void UpdateMealRecipe(Meal meal,int userId);
        //end of methods to modify recipes

        Meal GetMeal(int mealId, int userId);
        // void SaveMealRecipe(MealRecipeViewModel mealRecipeViewModel);


    }
}
