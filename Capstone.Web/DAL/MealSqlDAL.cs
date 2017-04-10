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

       

        public void SaveMeal(Meal meal, int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    meal.MealId = conn.QueryFirst<int>("INSERT INTO meal VALUES (@meal_name); Select CAST(SCOPE_IDENTITY() as int);",
                        new { meal_name = meal.MealName });

                    foreach (var recipe in meal.RecipeIds)
                    {
                        conn.Execute("Insert into meal_recipe values(@mealId, @recipeId, @userId, @mealType);",
                            new { mealId = meal.MealId, recipeId = recipe, userId = userId, mealType = meal.MealTypes });
                    }
                }
                
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        

    }

   
}





