using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.Models.ViewModels;
using System.Data.SqlClient;
using Dapper;



namespace Capstone.Web.DAL
{
    public class RecipeSqlDAL : IRecipeDAL
    {
        private readonly string connectionString;
        private const string SqlGetUserRecipes = @"Select * from recipe where user_id=@userId order by recipe_name;";
        private const string SqlGetTop10Recipes = @"Select top 10 * from recipe order by recipe_id desc;";
        private const string SqlGetTop10UserRecipes = @"Select top 10 * from recipe where user_id=@userId order by recipe_id desc;";
        private const string SqlGetUserRecipeIngredient = @"select ingredient.ingredient_name,recipe_ingredient.quantity,recipe_ingredient.measurement from recipe_ingredient
                                                          inner join ingredient on recipe_ingredient.ingredient_id=ingredient.ingredient_id and recipe_ingredient.recipe_id=@recipeId;";

        private const string SqlGetUserRecipeSteps = @"select steps from preparation_steps inner join recipe on recipe.recipe_id=@recipeId;";
        private const string SqlGetRecipe = @"select * from recipe where user_id=@userId and recipe_id=@recipeId;";
        private const string SqlUpdateRecipe = @"update recipe set recipe_name=@recipe_name,recipe_type = @recipe_type, image_name=@image_name, recipe_description=@recipe_description, cook_time=@cook_time where user_id=@user_id and recipe_id=@recipe_id;";

        public RecipeSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<string> GetRecipeNames(int userId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    return conn.Query<string>
                    ("SELECT recipe_name FROM recipe WHERE user_id = @userIdValue",
                        new { userIdValue = userId }).ToList();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public List<Recipe> GetAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=mealplanner;User ID=te_student;Password=sqlserver1"))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from recipe", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Recipe r = new Recipe()
                        {

                            Name = Convert.ToString(reader["recipe_name"]),
                            Description = Convert.ToString(reader["recipe_description"]),
                            RecipeId = Convert.ToInt32(reader["recipe_id"]),
                            ImageName = Convert.ToString(reader["image_name"]),

                            CookTimeInMinutes = Convert.ToInt32(reader["cook_time"]),


                        };

                        recipes.Add(r);
                    };
                    return recipes;

                }


            }
            catch (Exception)
            {

                throw;
            }


        }
        public Recipe GetRecipe(int recipeId, int userId)
        {
            Recipe r = new Recipe();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlGetRecipe, conn);
                    cmd.Parameters.AddWithValue("@recipeid", recipeId);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        r.UserId = Convert.ToInt32(reader["user_id"]);
                        r.RecipeId = Convert.ToInt32(reader["recipe_id"]);
                        r.Name = Convert.ToString(reader["recipe_name"]);
                        r.RecipeType = Convert.ToString(reader["recipe_type"]);
                        r.ImageName = Convert.ToString(reader["image_name"]);
                        r.Description = Convert.ToString(reader["recipe_description"]);
                        r.CookTimeInMinutes = Convert.ToInt32(reader["cook_time"]);

                    }

                }
                return r;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public void SaveRecipe(Recipe recipe)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    recipe.RecipeId = conn.QueryFirst<int>("Insert INTO recipe VALUES(@nameValue, @recipeTypeValue,@imageNameValue,@descriptionValue, @cookTimeInMinutes, @userIdValue); SELECT CAST (SCOPE_IDENTITY() as int);",
                        new { nameValue = recipe.Name, recipeTypeValue = recipe.RecipeType, imageNameValue = recipe.ImageName, descriptionValue = recipe.Description, cookTimeInMinutes = recipe.CookTimeInMinutes, userIdValue = recipe.UserId });

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<Recipe> GetUsersRecipes(int userId)
        {
            List<Recipe> userRecipes = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlGetUserRecipes, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Recipe r = new Recipe();
                        r.RecipeId = Convert.ToInt32(reader["recipe_id"]);
                        r.Name = Convert.ToString(reader["recipe_name"]);
                        r.ImageName = Convert.ToString(reader["image_name"]);
                        userRecipes.Add(r);
                    }
                    return userRecipes;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void UpdateRecipe(Recipe recipe)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlUpdateRecipe, conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipe.RecipeId);
                    cmd.Parameters.AddWithValue("@user_id", recipe.UserId);
                    cmd.Parameters.AddWithValue("@recipe_name", recipe.Name);
                    cmd.Parameters.AddWithValue("@recipe_type", recipe.RecipeType);
                    cmd.Parameters.AddWithValue("@image_name", recipe.ImageName);
                    cmd.Parameters.AddWithValue("@recipe_description", recipe.Description);
                    cmd.Parameters.AddWithValue("@cook_time", recipe.CookTimeInMinutes);


                    cmd.ExecuteNonQuery(); 
                }
                
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
        public List<Recipe> GetTop10RecentlyAddedRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlGetTop10Recipes, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        Recipe r = new Recipe();
                        r.UserId = Convert.ToInt32(reader["user_id"]);
                        r.RecipeId = Convert.ToInt32(reader["recipe_id"]);
                        r.Name = Convert.ToString(reader["recipe_name"]);
                        r.RecipeType = Convert.ToString(reader["recipe_type"]);
                        r.ImageName = Convert.ToString(reader["image_name"]);
                        r.Description = Convert.ToString(reader["recipe_description"]);
                        r.CookTimeInMinutes = Convert.ToInt32(reader["cook_time"]);
                        recipes.Add(r);
                    }
                }
                    
            }
            catch (SqlException ex)
            {

            }
            return recipes;
        }

        public List<Recipe> GetTop10RecentlyAddedUserRecipes(int userId)
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlGetTop10UserRecipes, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Recipe r = new Recipe();
                        r.UserId = Convert.ToInt32(reader["user_id"]);
                        r.RecipeId = Convert.ToInt32(reader["recipe_id"]);
                        r.Name = Convert.ToString(reader["recipe_name"]);
                        r.RecipeType = Convert.ToString(reader["recipe_type"]);
                        r.ImageName = Convert.ToString(reader["image_name"]);
                        r.Description = Convert.ToString(reader["recipe_description"]);
                        r.CookTimeInMinutes = Convert.ToInt32(reader["cook_time"]);
                        recipes.Add(r);
                    }
                }

            }
            catch (SqlException ex)
            {

            }
            return recipes;
        }

    }
}
