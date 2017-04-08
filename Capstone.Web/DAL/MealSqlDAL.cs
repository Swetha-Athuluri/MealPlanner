using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Dapper;
using System.Data.SqlClient;

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

        public void SaveMeal()
        {
            throw new NotImplementedException();
        }

    }
}





