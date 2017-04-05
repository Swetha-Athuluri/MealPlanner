using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using Dapper;



namespace Capstone.Web.DAL
{
    public class RecipeSqlDAL : IRecipeDAL
    {
        private readonly string connectionString;
        public RecipeSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
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
        public Recipe GetRecipe(int recipeId)
        {
            return null;
        }
        public void SaveRecipe(Recipe newRecipe, List<string> steps)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    newRecipe.RecipeId = conn.QueryFirst<int>("Insert INTO recipe VALUES(@nameValue, @recipeTypeValue,@imageNameValue,@descriptionValue, @cookTimeInMinutes, @userIdValue); SELECT CAST (SCOPE_IDENTITY() as int);",
                        new { nameValue = newRecipe.Name, recipeTypeValue = newRecipe.RecipeType, imageNameValue = newRecipe.Name, descriptionValue = newRecipe.Description, cookTimeInMinutes = newRecipe.CookTimeInMinutes, userIdValue = newRecipe.UserId });

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<Recipe> GetUsersRecipes(User userName)
        {
            return null;
        }
    }
}