using System;
using System.Data.SqlClient;
using FinanceManagement.IFinaceRepository.Exceptions;
using FinanceManagement.IFinaceRepository.Model;
using FinanceManagement.IFinaceRepository.Util;

namespace FinanceManagement.IFinaceRepository.Repository
{
   public class UserService : IUserService
    {
        SqlConnection sql = null;
        SqlCommand cmd = null;

        public UserService()
        {
            sql = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public int CreateUser(User user)
        {
            int rowsAffected = 0;
            try
            {
                
                sql.Open();
                cmd.CommandText = "INSERT INTO Users (UserName, Email, Password, ContactNumber) " +
                               "VALUES (@UserName, @Email, @Password, @ContactNumber)";

                cmd.Parameters.AddWithValue("@UserName", user.Username);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@ContactNumber", user.ContactNumber);
                cmd.Connection = sql;
                 rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating user: " + ex.Message);
                return rowsAffected;
            }
            finally
            {
                sql.Close();
            }

        }

        public bool DeleteUser(int userId)
        {
            try
            {
                sql.Open();
                string query = "DELETE FROM Users WHERE user_id = @UserId";
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Connection = sql;
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new UserNotFoundException($"User with ID {userId} not found.");
                }

                return rowsAffected > 0;
            }            
            catch (UserNotFoundException ex)
            {
                Console.WriteLine("Error deleting user: " + ex.Message);
                return false;
            }
            catch(SqlException ex)
            {
                Console.WriteLine("Wrong Info");
                return false;
            }
            finally
            {
                sql.Close();
            }
        }

    }
}
