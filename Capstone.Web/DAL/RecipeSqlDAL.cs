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
        public void SaveRecipe(RecipeViewModel rvm)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    rvm.RecipeId = conn.QueryFirst<int>("Insert INTO recipe VALUES(@nameValue, @recipeTypeValue,@imageNameValue,@descriptionValue, @cookTimeInMinutes, @userIdValue); SELECT CAST (SCOPE_IDENTITY() as int);",
                        new { nameValue = rvm.RecipeName, recipeTypeValue = rvm.RecipeType, imageNameValue = rvm.RecipeName, descriptionValue = rvm.RecipeDescription, cookTimeInMinutes = rvm.RecipeCookTimeInMinutes, userIdValue = rvm.UserId});
                    for (int i = 0; i < rvm.Steps.Count; i++)
                    {
                        conn.Execute("Insert Into preparation_steps VALUES(@recipeid, @step);",
                        new { recipeid = rvm.RecipeId, step = rvm.Steps[i] });
                    }
                    for (int i = 0; i < rvm.IngredientQuantity.Count; i++)
                    {
                        rvm.IngredientId=conn.QueryFirst<int>("Insert Into ingredient VALUES(@ingredient_name);Select cast(scope_identity() as int);",
                        new { ingredient_name=rvm.IngredientName[i] });
                        conn.Execute("Insert into recipe_ingredient values(@recipe_id,@ingredient_id,@quantity,@measurement);",
                            new { recipe_id = rvm.RecipeId, ingredient_id = rvm.IngredientId, quantity = rvm.IngredientQuantity[i], measurement = rvm.IngredientMeasurementOptions[i] });
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public List<RecipeViewModel> GetUsersRecipes(int userId)
        {
            return null;
            
        }
    }
}