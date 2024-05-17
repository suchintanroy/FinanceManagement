using System;
using FinanceManagement.IFinaceRepository.Util;
using System.Data.SqlClient;
using FinanceManagement.IFinaceRepository.Repository;
using FinanceManagement.IFinaceRepository.Service;
namespace FinanceManagement
{
    internal class FinanceApp
    {
        private UserService userService;
        private UserExpense userExpense;
        public bool isLoggedIn;
        SqlConnection sql = null;
        SqlCommand cmd = null;

        public FinanceApp()
        {
            userService = new UserService();
            userExpense = new UserExpense();
            isLoggedIn = false;
            sql = new SqlConnection(DbConnUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public void LoginOrCreateUser()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\t\t\t\t\t\t\tFINANCEMANAGEMENT");
                Console.WriteLine("Login or Create User");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Create User");

                
                Console.WriteLine("Enter your choice:");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                       
                        Login();
                        exit = isLoggedIn;
                        break;
                    case 2:
                       
                        UserManagement.CreateUser(userService); 
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void DisplayMainMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\t\t\t\t\t\t\tWelcome Buddy");
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Create Expense");
                Console.WriteLine("2. Delete User");
                Console.WriteLine("3. Delete Expense");
                Console.WriteLine("4. Get All Expenses");
                Console.WriteLine("5. Update Expense");
                Console.WriteLine("6. Reports Generation");
                Console.WriteLine("7. Search Expense");
                Console.WriteLine("Enter your choice:");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ExpenseManagement.CreateExpense(userExpense);

                        break;
                    case 2:
                        UserManagement.DeleteUser(userService);
                        break;
                    case 3:
                        ExpenseManagement.DeleteExpense(userExpense);
                        break;
                    case 4:
                        ExpenseManagement.GetAllExpenses(userExpense);
                        break;
                    case 5:
                        ExpenseManagement.UpdateExpense(userExpense);

                        break;
                    case 6:
                        ExpenseManagement.PromptUserForExpenseReport(userExpense);
                        break;
                    case 7:
                      ExpenseManagement.PromptUserForExpenseSearch( userExpense);
                        break;
                    case 8:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void Login()
        {
            Console.WriteLine("User Login");
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            string connectionString = DbConnUtil.GetConnectionString();

           
                sql.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND Password = @Password";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                      cmd.Connection = sql;
                  int count = (int)cmd.ExecuteScalar();
            
                if (count > 0)
                {

                    Console.WriteLine("Login successful!");
                    isLoggedIn = true;
                }
                else
                {
                    Console.WriteLine("Invalid email or password. Please try again.");
                } }
           
        
        } }   