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
        public void SaveUser(User user)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    user.User_Id = conn.QueryFirst<int>("INSERT INTO users VALUES (@userNameValue, @emailValue, @passwordValue, @saltValue); SELECT CAST(SCOPE_IDENTITY() as int);",
                        new { userNameValue = user.Username, emailValue = user.Email, passwordValue = user.Password, saltValue = user.Salt });
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }
    }
}





