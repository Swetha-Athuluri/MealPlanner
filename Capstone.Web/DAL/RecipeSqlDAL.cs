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

        //private const string SqlGetUserRecipe = @"select recipe.recipe_name, recipe.image_name, recipe.recipe_type, recipe.recipe_description, recipe.cook_time,ingredient.ingredient_name,recipe_ingredient.quantity,recipe_ingredient.measurement,preparation_steps.steps
        //                                          from recipe_ingredient
        //                                          inner join recipe on recipe.recipe_id=recipe_ingredient.recipe_id
        //                                          inner join ingredient on ingredient.ingredient_id=recipe_ingredient.ingredient_id
        //                                          inner join preparation_steps on preparation_steps.recipe_id = recipe.recipe_id and recipe.user_id = @userId;";

        //private const string SqlGetUserRecipe = @"select recipe_id,recipe_name,recipe_type,image_name,recipe_description,cook_time,user_id from recipe where user_id=@userId;";
        private const string SqlGetUserRecipes = @"Select * from recipe where user_id=@userId;";
        private const string SqlGetUserRecipeIngredient = @"select ingredient.ingredient_name,recipe_ingredient.quantity,recipe_ingredient.measurement from recipe_ingredient
                                                          inner join ingredient on recipe_ingredient.ingredient_id=ingredient.ingredient_id and recipe_ingredient.recipe_id=@recipeId;";

        private const string SqlGetUserRecipeSteps = @"select steps from preparation_steps inner join recipe on recipe.recipe_id=@recipeId;";
        private const string SqlGetRecipe = @"select * from recipe where user_id=@userId and recipe_id=@recipeId;";
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
        public Recipe GetRecipe(int recipeId,int userId)
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
        public void SaveRecipe(RecipeViewModel rvm)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    rvm.RecipeId = conn.QueryFirst<int>("Insert INTO recipe VALUES(@nameValue, @recipeTypeValue,@imageNameValue,@descriptionValue, @cookTimeInMinutes, @userIdValue); SELECT CAST (SCOPE_IDENTITY() as int);",
                        new { nameValue = rvm.RecipeName, recipeTypeValue = rvm.RecipeType, imageNameValue = rvm.RecipeName, descriptionValue = rvm.RecipeDescription, cookTimeInMinutes = rvm.RecipeCookTimeInMinutes, userIdValue = rvm.UserId });
                    for (int i = 0; i < rvm.Steps.Count; i++)
                    {
                        conn.Execute("Insert Into preparation_steps VALUES(@recipeid, @step);",
                        new { recipeid = rvm.RecipeId, step = rvm.Steps[i] });
                    }
                    //for (int i = 0; i < rvm.IngredientQuantity.Count; i++)
                    //{
                    //    //changed ingredientid from int to list .. for 
                    //    rvm.IngredientId.Add(conn.QueryFirst<int>("Insert Into ingredient VALUES(@ingredient_name);Select cast(scope_identity() as int);",
                    //    new { ingredient_name = rvm.IngredientName[i] }));
                    //    conn.Execute("Insert into recipe_ingredient values(@recipe_id,@ingredient_id,@quantity,@measurement);",
                    //        new { recipe_id = rvm.RecipeId, ingredient_id = rvm.IngredientId, quantity = rvm.IngredientQuantity[i], measurement = rvm.IngredientMeasurementOptions[i] });
                    //}
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

        //List<RecipeViewModel> recipes = new List<RecipeViewModel>();
        //try
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(SqlGetUserRecipe, conn);
        //        cmd.Parameters.AddWithValue("@userId", userId);
        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            RecipeViewModel r = new RecipeViewModel();

        //            r.RecipeName = Convert.ToString(reader["recipe_name"]);
        //            r.RecipeImageName = Convert.ToString(reader["image_name"]);
        //            r.RecipeType = Convert.ToString(reader["recipe_type"]);
        //            r.RecipeDescription = Convert.ToString(reader["recipe_description"]);
        //            r.RecipeCookTimeInMinutes = Convert.ToInt32(reader["cook_time"]);
        //            r.UserId = Convert.ToInt32(reader["user_id"]);
        //            r.RecipeId = Convert.ToInt32(reader["recipe_id"]);


        //            SqlCommand ingredientCmd = new SqlCommand(SqlGetUserRecipeIngredient, conn);
        //            ingredientCmd.Parameters.AddWithValue("@recipeId", r.RecipeId);
        //            SqlDataReader ingredientReader = ingredientCmd.ExecuteReader();
        //            while (ingredientReader.Read())
        //            {
        //                //Ingredient i = new Ingredient();
        //                //{
        //                //    i.Id = Convert.ToInt32(ingredientReader["ingredient_id"]);
        //                //    i.Name = Convert.ToString(ingredientReader["ingredient_name"]);
        //                //}
        //                r.IngredientId.Add(Convert.ToInt32(ingredientReader["ingredient_id"]));
        //                r.IngredientName.Add(Convert.ToString(ingredientReader["ingredient_name"]));
        //                r.IngredientQuantity.Add(Convert.ToInt32(ingredientReader["qunatity"]));
        //                r.IngredientMeasurementOptions.Add(Convert.ToString(ingredientReader["measurement"]));
        //            }

        //            SqlCommand preparationStepsCmd = new SqlCommand(SqlGetUserRecipeSteps, conn);
        //            preparationStepsCmd.Parameters.AddWithValue("@recipeId", r.RecipeId);
        //            SqlDataReader preparationStepsReader = preparationStepsCmd.ExecuteReader();
        //            while (preparationStepsReader.Read())
        //            {
        //                r.Steps.Add(Convert.ToString(preparationStepsReader["steps"]));
        //            }

        //            recipes.Add(r);
        //        }
        //    }
        //    return recipes;
        //}
        //catch (Exception ex)
        //{
        //    throw;
        //}
    }
}
