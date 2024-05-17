using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManagement.IFinaceRepository.Model;

namespace FinanceManagement.IFinaceRepository.Repository
{
   public interface IUserExpense
    {
        public bool CreateExpense(Expense expense);
        public bool DeleteExpense(int expenseId);
        public int GetUserIdByUsername(string username);

        public void GenerateExpenseReport(int userId, DateTime startDate, DateTime endDate);
        public List<Expense> GetAllExpenses(int userId);
        public void SearchExpenseById(int expenseId);
      public bool UpdateExpense(int userId, Expense expense);
    }
}
