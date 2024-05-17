using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManagement.IFinaceRepository.Exceptions;
using FinanceManagement.IFinaceRepository.Model;
using FinanceManagement.IFinaceRepository.Util;





namespace FinanceManagement.IFinaceRepository.Repository
{
    public class UserExpense : IUserExpense
    {
        SqlConnection sql = null;
        SqlCommand cmd = null;
        public UserExpense()
        {

            sql = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
        }
        public bool CreateExpense(Expense expense)
        {
            try
            {
                sql.Open();
                cmd.CommandText = "INSERT INTO Expenses (user_id, category_id, amount, StartDate,EndDate, description) " +
                   "VALUES (@userId, @categoryId, @amount, @startDate,@endDate, @description)";
                cmd.Parameters.AddWithValue("@userID", expense.UserId);
                cmd.Parameters.AddWithValue("@Amount", expense.Amount);
                cmd.Parameters.AddWithValue("@categoryID", expense.CategoryId);
                cmd.Parameters.AddWithValue("@startDate", expense.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", expense.EndDate);
                cmd.Parameters.AddWithValue("@Description", expense.Description);

                cmd.Connection = sql;

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating Expense: " + ex.Message);
                return false;
            }
            finally
            {
                sql.Close();
            }
        }
        public bool DeleteExpense(int expenseId)
        {
            try
            {
                sql.Open();
                cmd.CommandText = "DELETE FROM Expenses WHERE expense_id = @ExpenseId";
                cmd.Parameters.AddWithValue("@ExpenseId", expenseId);
                cmd.Connection = sql;
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected == 0)
                {
                    throw new ExpenseNotFoundException($"Expense with ID {expenseId} not found.");
                }
                return true;
            }
            catch (ExpenseNotFoundException ex)
            {
                Console.WriteLine("Error deleting Expense: " + ex.Message);
                return false;
            }
            finally
            {
                sql.Close();
            }
        }
        public List<Expense> GetAllExpenses(int userId)
        {

            List<Expense> expenses = new List<Expense>();
            try
            {
                sql.Open();
                 
                cmd.CommandText = "SELECT * FROM Expenses WHERE user_id = @UserId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Connection = sql;
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new ExpenseNotFoundException($"No expenses found for user with ID {userId}.");
                }
                while (reader.Read())
                {
                    Expense expense = new Expense
                    {
                        ExpenseId = Convert.ToInt32(reader["expense_id"]),
                        UserId = Convert.ToInt32(reader["user_id"]),
                        Amount = Convert.ToDecimal(reader["amount"]),
                        CategoryId = Convert.ToInt32(reader["category_id"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        Description = Convert.ToString(reader["description"])
                    };
                    expenses.Add(expense);
                }
                reader.Close();
            }
            catch (ExpenseNotFoundException ex)
            {
                Console.WriteLine("Error in retrieving expenses:" + ex.Message);
            }
            finally
            {
                sql.Close();
            }

            return expenses;
        }

        public bool UpdateExpense(int userId, Expense expense)
        {
            try
            {
                sql.Open();
                cmd.CommandText = @"UPDATE Expenses SET amount = @amount, category_id = @categoryId, StartDate = @startDate,EndDate = @endDate, description = @description
                                  WHERE expense_id = @expenseId AND user_id = @userId";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@amount", expense.Amount);
                cmd.Parameters.AddWithValue("@categoryId", expense.CategoryId);
                cmd.Parameters.AddWithValue("@Startdate", expense.StartDate);
                cmd.Parameters.AddWithValue("@Enddate", expense.EndDate);
                cmd.Parameters.AddWithValue("@description", expense.Description);
                cmd.Parameters.AddWithValue("@expenseId", expense.ExpenseId);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Connection = sql;
                int rowsAffectedExpense = cmd.ExecuteNonQuery();
                return rowsAffectedExpense > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating expense: " + ex.Message);
                return false;
            }
            finally
            {
                sql.Close();
            }
        }
        public int GetUserIdByUsername(string username)
        {
            try
            {
                sql.Open();
                cmd.CommandText = "SELECT user_id FROM Users WHERE username = @UserName";
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Connection = sql;

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    Console.WriteLine($"User with username {username} not found.");
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching user ID: " + ex.Message);
                return -1;
            }
            finally
            {
                sql.Close();
            }
        }
        public void GenerateExpenseReport(int userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                sql.Open();
                cmd.CommandText = @"SELECT expense_id, amount, StartDate, EndDate, category_id, Description
                         FROM Expenses 
                         WHERE user_id = @UserId AND StartDate >= @StartDate AND EndDate <= @EndDate 
                         ORDER BY StartDate";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@StartDate", startDate);
                cmd.Parameters.AddWithValue("@EndDate", endDate);
               
                cmd.Connection = sql;
                SqlDataReader reader = cmd.ExecuteReader();
                Console.WriteLine("Expense Report:");
                while (reader.Read())
                {
                    Console.WriteLine($"Expense ID: {reader["expense_id"]}, Amount: {reader["amount"]}, " +
                              $"Start Date: {reader["StartDate"]}, End Date: {reader["EndDate"]}, " +
                              $"Category ID: {reader["category_id"]}, Description: {reader["Description"]}");
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error generating expense report: " + ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }

        public void SearchExpenseById(int expenseId)
        {
            try
            {
                sql.Open();
                cmd.CommandText = "SELECT expense_id, amount, StartDate, EndDate, category_id, Description FROM Expenses WHERE expense_id = @ExpenseId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ExpenseId", expenseId);
                cmd.Connection = sql;
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Expense expense = new Expense
                    {
                        ExpenseId = (int)reader["expense_id"],
                        Amount = (decimal)reader["amount"],
                        StartDate = (DateTime)reader["StartDate"],
                        EndDate = (DateTime)reader["EndDate"],
                        CategoryId = (int)reader["category_id"],
                        Description = (string)reader["Description"]
                    };

                    Console.WriteLine($"Expense found with ID {expenseId}:");
                    Console.WriteLine($"Amount: {expense.Amount}, Start Date: {expense.StartDate}, End Date: {expense.EndDate}, Category ID: {expense.CategoryId}, Description: {expense.Description}");
                }
                else
                {
                    throw new ExpenseNotFoundException($"Expense with ID {expenseId} not found.");
                }
            }
            catch (ExpenseNotFoundException ex)
            {
                Console.WriteLine("Error searching Expense: " + ex.Message);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }

    }


}




















