using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class PreparationStepsSqlDAL : IPreparationStepsDAL
    {
        private readonly string connectionString;
        private const string SqlRecipeIngredientQuery= @"select * from preparation_steps where recipe_id=@recipe_id;";

        public PreparationStepsSqlDAL(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<PreparationSteps> GetPreparationStepsForRecipe(int recipeId)
        {
            List<PreparationSteps> preparationSteps = new List<PreparationSteps>();

           try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SqlRecipeIngredientQuery,conn);
                    cmd.Parameters.AddWithValue("@recipe_id", recipeId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        PreparationSteps step = new PreparationSteps();
                        step.RecipeId = Convert.ToInt32(reader["recipe_id"]);
                        step.StepId = Convert.ToInt32(reader["step_id"]);
                        step.Steps = Convert.ToString(reader["steps"]);
                        preparationSteps.Add(step);
                    }

                }
                return preparationSteps;
            }
            catch (SqlException ex)
            {
                throw;
            }

        }
    }
}