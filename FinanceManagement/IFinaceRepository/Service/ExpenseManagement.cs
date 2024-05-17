using FinanceManagement.IFinaceRepository.Model;
using FinanceManagement.IFinaceRepository.Repository;

namespace FinanceManagement.IFinaceRepository.Service
{
    internal class ExpenseManagement
    {
       // 1.Create Expense
        public static void CreateExpense(UserExpense userExpense)
        {
            
            Console.WriteLine("Enter UserID:");
            int userId = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter CatagoryID:");
            int categoryId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Amount:");
            decimal amount = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Enter StartDate (yyyy-MM-dd):");
            DateTime Startdate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter EndDate (yyyy-MM-dd):");
            DateTime Enddate = DateTime.Parse(Console.ReadLine());



            Console.WriteLine("Enter Description:");
            string description = Console.ReadLine();

            Expense newExpense = new Expense
            {
                UserId = userId,
                CategoryId = categoryId,
                Amount = amount,
                StartDate = Startdate,
                EndDate = Enddate,
                Description = description
            };

            bool expenseCreated = userExpense.CreateExpense(newExpense);

            if (expenseCreated)
            {
                Console.WriteLine("Your Expense created successfully!");
              
            }
            else
            {
                Console.WriteLine("Failed to create Expense. Please try again.");
            
            }
           
        }
        //2. Delete Expense
        public static void DeleteExpense(UserExpense userExpense)
        {
            Console.WriteLine("Enter the EXPENSE ID to delete:");
            int expenseId = int.Parse(Console.ReadLine());

            bool expenseDeleted = userExpense.DeleteExpense(expenseId);

            if (expenseDeleted)
            {
                Console.WriteLine($"Expense with ID {expenseId} deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete expense. Please try again.");
            }
        }
        //3.Getting All Expense
        public static void GetAllExpenses(UserExpense userExpense)
        {
            Console.WriteLine("Enter the USER-ID:");
            int userId = Convert.ToInt32(Console.ReadLine());
            List<Expense> expenses = userExpense.GetAllExpenses(userId);

            if (expenses != null && expenses.Any())
            {
                Console.WriteLine("Expenses for User using UserID " + userId);
                foreach (var expense in expenses)
                {
                    Console.WriteLine($"Expense ID: {expense.ExpenseId}, Amount: {expense.Amount}, StartDate: {expense.StartDate},EndDate: {expense.EndDate}, Description: {expense.Description}");
                }
            }
            else
            {
                Console.WriteLine("No expenses Found for UserID " + userId);
            }
        }
       // 4. Updating Expense
        public static void UpdateExpense(UserExpense userExpense)
        {
            try
            {
                
                Console.WriteLine("Enter UserID:");
                int userId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter ExpenseID:");
                int expenseId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter CategoryID:");
                int categoryId = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter Amount:");
                decimal amount = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Enter StartDate (yyyy-MM-dd):");
                DateTime startdate = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Enter EndDate (yyyy-MM-dd):");
                DateTime enddate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Enter Description:");
                string description = Console.ReadLine();

                
                Expense updatedExpense = new Expense
                {
                    UserId = userId,
                    ExpenseId = expenseId,
                    CategoryId = categoryId,
                    Amount = amount,
                    StartDate = startdate,
                    EndDate = enddate,
                    Description = description
                };

                
                bool expenseUpdated = userExpense.UpdateExpense(userId, updatedExpense);

                
                if (expenseUpdated)
                {
                    Console.WriteLine("Expense updated successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to update expense. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        //5. Generating Expense Report


        public static void PromptUserForExpenseReport(UserExpense userExpense)
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();

            int userId = userExpense.GetUserIdByUsername(username);
            if (userId == -1)
            {
                return;
            }

            Console.WriteLine("Enter the start date (yyyy-mm-dd):");
            string startDateInput = Console.ReadLine();
            Console.WriteLine("Enter the end date (yyyy-mm-dd):");
            string endDateInput = Console.ReadLine();

            if (!DateTime.TryParse(startDateInput, out DateTime startDate) || !DateTime.TryParse(endDateInput, out DateTime endDate))
            {
                Console.WriteLine("Invalid date format. Please enter the dates in yyyy-mm-dd format.");
                return;
            }

            if (startDate > endDate)
            {
                Console.WriteLine("Start date must be before end date.");
                return;
            }

            userExpense.GenerateExpenseReport(userId, startDate, endDate);
        
     }
        public static void PromptUserForExpenseSearch(UserExpense userExpense)
        {
            Console.WriteLine("Enter the expense ID to search:");
            if (int.TryParse(Console.ReadLine(), out int expenseId))
            {
                userExpense.SearchExpenseById(expenseId);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid expense ID.");
            }
        }

    
}

}

