using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;



namespace Capstone.Web.DAL
{
    public class RecipeSqlDAL
    {
        public List<Recipe> GetAllRecipe()
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
                            PreparationSteps = Convert.ToString(reader["preparation_steps"]),
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
    }
}