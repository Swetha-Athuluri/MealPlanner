using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class IngredientSqlDAL : IIngredientDAL
    {
        public List<Ingredient> GetAllIngredients()
        {
            List<Ingredient> listOfIngredients = new List<Ingredient>();
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=mealplanner;User ID=te_student;Password=sqlserver1"))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select * from ingredient", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Ingredient i = new Ingredient()
                        {
                            Name = Convert.ToString(reader["ingredient_name"]),
                            Id = int.Parse(Convert.ToString(reader["ingredient_id"]))
                            };
                        listOfIngredients.Add(i);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return listOfIngredients;
        }

        public string GetIngredientName(int ingredientId)
        {
            return null;
        }
    }
}