using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Dapper;
using System.Data.SqlClient;
using Capstone.Web.Models.ViewModels;

namespace Capstone.Web.DAL
{
    public class MealSqlDAL : IMealDAL
    {
        private readonly string connectionString;

        public MealSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Recipe> GetAllMeals(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    return conn.Query<Recipe>
                    ("SELECT * FROM meal WHERE userId = @userIdValue",
                        new { userIdValue = userId }).ToList();     
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void SaveMeal(MealRecipeViewModel mealRecipeViewModel)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    mealRecipeViewModel.MealId = conn.QueryFirst<int>("INSERT INTO meal VALUES (@meal_name); Select CAST(SCOPE_IDENTITY() as int);",
                        new { meal_name = mealRecipeViewModel.MealName });
                    //user.User_Id = conn.QueryFirst<int>("INSERT INTO users VALUES (@userNameValue, @emailValue, @passwordValue, @saltValue); SELECT CAST(SCOPE_IDENTITY() as int);",
                    //    new { userNameValue = user.Username, emailValue = user.Email, passwordValue = user.Password, saltValue = user.Salt });
                }
                //using (SqlConnection conn = new SqlConnection(connectionString))
                //{ 
                //    foreach (var recipe in mealRecipeViewModel.ListOfRecipies)
                //    {
                //        conn.QueryFirst("Insert into meal_recipe values(@mealId,@recipeId,@userId,@mealType);",
                //            new { mealId = mealRecipeViewModel.MealId, recipeId = recipe.RecipeId, userId = mealRecipeViewModel.UserId, mealType = mealRecipeViewModel.MealType });
                //    }
                //}
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void SaveUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    user.User_Id = conn.QueryFirst<int>("INSERT INTO users VALUES (@userNameValue, @emailValue, @passwordValue, @saltValue); SELECT CAST(SCOPE_IDENTITY() as int);",
                        new { userNameValue = user.Username, emailValue = user.Email, passwordValue = user.Password, saltValue = user.Salt });
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

    }

   
}





