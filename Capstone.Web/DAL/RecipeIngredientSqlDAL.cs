using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using Capstone.Web.Models.ViewModels;
using Dapper;

namespace Capstone.Web.DAL
{
    public class RecipeIngredientSqlDAL : IRecipeIngredientDAL
    {
        private readonly string connectionString;
        private const string SqlGetUserRecipeIngredient = @"select * from recipe_ingredient where recipe_id=@recipe_id;";
        private const string SqlModifyRecipeIngredient = @"update recipe_ingredient set ingredient_name=@ingredient_name, quantity=@quantity, measurement=@measurement where recipe_id=@recipe_id and ingredient_name=@ingredient_name;";
        public RecipeIngredientSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<RecipeIngredient> GetRecipeIngredients(int recipeId)
        {
            List<RecipeIngredient> recipeIngredient = new List<RecipeIngredient>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlGetUserRecipeIngredient, conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RecipeIngredient r = new RecipeIngredient();
                        r.Ingredient_Name = Convert.ToString(reader["ingredient_name"]);
                        r.Measurement = Convert.ToString(reader["measurement"]);
                        r.Quantity = Convert.ToInt32(reader["quantity"]);

                        recipeIngredient.Add(r);

                    }

                }
                return recipeIngredient;
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void SaveRecipeIngredients(List<RecipeIngredient> recipeIngredients, int recipe_id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    foreach (var recipeIngredient in recipeIngredients)
                    {

                        conn.Execute("INSERT INTO recipe_ingredient VALUES (@recipeIdValue, @nameValue,  @quantityValue, @measurementValue);",
                           new { recipeIdValue = recipe_id, nameValue = recipeIngredient.Ingredient_Name, quantityValue = recipeIngredient.Quantity, measurementValue = recipeIngredient.Measurement });

                    }
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public bool ModifyRecipeIngredient(List<RecipeIngredient> recipeIngredients, int existingIngredientCount, int modifiedIngredientCount)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    if (modifiedIngredientCount > existingIngredientCount)
                    {
                        for (int i = 1; i < existingIngredientCount; i++)
                        {

                        }

                    }

                    
                }


               
            }
             return true;


            catch
            {

            }
        }
    }
}