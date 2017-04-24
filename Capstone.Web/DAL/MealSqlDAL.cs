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

        private const string SqlDeleteMealRecipe = @"Delete from meal_recipe where meal_id=@mealId and user_id=@userId;";
        private const string SqlDeleteMeal = @"Delete from meal where meal_id=@meal_Id;";
        private const string SqlUpdateMeal = @"Update meal set meal_name=@mealName where meal_Id=@mealId;";

        public MealSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //public List<Meal> GetAllMeals(int userId)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            return conn.Query<Meal>
        //            ("SELECT * from meal INNER JOIN meal_recipe on meal.meal_id = meal_recipe.meal_id where user_id = @userIdValue",
        //                new { userIdValue = userId }).ToList();
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw;
        //    }
        //}
        public Meal GetMeal(int mealId, int userId)
        {
            try
            {
                Meal m = new Meal();
                m.MealTypes = new List<string>();
                m.RecipeIds = new List<int>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(("SELECT * from meal INNER JOIN meal_recipe on meal.meal_id = meal_recipe.meal_id where meal.meal_id = @mealValueId AND user_id = @userIdValue"), conn);
                    cmd.Parameters.AddWithValue("@mealValueId", mealId);
                    cmd.Parameters.AddWithValue("@userIdValue", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        m.MealId = mealId;
                        m.MealName = Convert.ToString(reader["meal_name"]);
                        m.MealTypes.Add(Convert.ToString(reader["meal_type"]));
                        m.RecipeIds.Add(Convert.ToInt32(reader["recipe_id"]));
                    }
                    reader.Close();

                    SqlCommand cmd2 = new SqlCommand("Select recipe_name, meal_recipe.recipe_id as recipe_id, meal_type, image_name from recipe inner join meal_recipe on recipe.recipe_id = meal_recipe.recipe_id where meal_id = @mealIdValue", conn);
                    cmd2.Parameters.AddWithValue("@mealIdValue", m.MealId);

                    SqlDataReader sdr = cmd2.ExecuteReader();
                    while (sdr.Read())
                    {
                        m.Recipes.Add(new MealRecipe()
                        {
                            MealType = Convert.ToString(sdr["meal_type"]),
                            RecipeName = Convert.ToString(sdr["recipe_name"]),
                            RecipeImageName = Convert.ToString(sdr["image_name"]),
                            RecipeId = Convert.ToInt32(sdr["recipe_id"])
                        });

                    }


                }
                return m;

            }
            catch (SqlException ex)
            {

                throw;
            }
        }
        public List<Meal> GetAllMeals(int userId)
        {
            try
            {

                List<Meal> meals = new List<Meal>();



                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Distinct meal.meal_id, meal_name from meal INNER JOIN meal_recipe on meal.meal_id = meal_recipe.meal_id where user_id = @userIdValue", connection);
                    cmd.Parameters.AddWithValue("@userIdValue", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Meal m = new Meal();
                        m.MealName = Convert.ToString(reader["meal_name"]);
                        m.MealId = Convert.ToInt32(reader["meal_id"]);
                        meals.Add(m);
                    }
                    reader.Close();
                    foreach (var meal in meals)
                    {
                        SqlCommand cmd2 = new SqlCommand("Select recipe_name, meal_recipe.recipe_id as recipe_id, meal_type, image_name from recipe inner join meal_recipe on recipe.recipe_id = meal_recipe.recipe_id where meal_id = @mealIdValue", connection);
                        cmd2.Parameters.AddWithValue("@mealIdValue", meal.MealId);

                        SqlDataReader sdr = cmd2.ExecuteReader();
                        while (sdr.Read())
                        {
                            if (meal.MealImageName == null)
                            {
                                meal.MealImageName = Convert.ToString(sdr["image_name"]);
                            }
                            meal.Recipes.Add(new MealRecipe()
                            {
                                MealType = Convert.ToString(sdr["meal_type"]),
                                RecipeName = Convert.ToString(sdr["recipe_name"]),
                                RecipeImageName = Convert.ToString(sdr["image_name"]),
                                RecipeId = Convert.ToInt32(sdr["recipe_id"])

                            });

                        }
                        sdr.Close();
                    }

                }
                return meals;

            }
            catch (SqlException ex)
            {

                throw;
            }
        }

        public void DeleteRecipesForMeal(int userId, int mealId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlDeleteMealRecipe, conn);
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("mealId", mealId);
                    cmd.ExecuteNonQuery();

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void UpdateMeal(int mealId,string mealName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlUpdateMeal, conn);
                    cmd.Parameters.AddWithValue("mealId", mealId);
                    cmd.Parameters.AddWithValue("mealName", mealName);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void UpdateMealRecipe(Meal meal,int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var counter = 0;
                    foreach (var recipe in meal.RecipeIds)
                    {
                        conn.Execute("Insert into meal_recipe values(@mealId, @recipeId, @userId, @mealType);",
                            new { mealId = meal.MealId, recipeId = recipe, userId = userId, mealType = meal.MealTypes[counter] });
                        counter++;
                    }
                }
            }
            catch(SqlException ex)
            {
                throw ex;
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
                    var counter = 0;
                    foreach (var recipe in meal.RecipeIds)
                    {
                        conn.Execute("Insert into meal_recipe values(@mealId, @recipeId, @userId, @mealType);",
                            new { mealId = meal.MealId, recipeId = recipe, userId = userId, mealType = meal.MealTypes[counter] });
                        counter++;
                    }
                }

            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void DeleteMealRecipe(int userId, int mealId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlDeleteMealRecipe, conn);
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("mealId", mealId);
                    cmd.ExecuteNonQuery();
                    //changed
                    SqlCommand cmd1 = new SqlCommand(SqlDeleteMeal, conn);
                    cmd1.Parameters.AddWithValue("meal_Id", mealId);
                    cmd1.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }


}





