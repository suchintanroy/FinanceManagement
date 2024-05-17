using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.IFinaceRepository.Model
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }   
        public string Description { get; set; }

        public Expense() { }

        public Expense(int expenseId, int userId, decimal amount, int categoryId, DateTime startDate ,DateTime endDate, string description)
        {
           ExpenseId = expenseId;
            UserId = userId;
            Amount = amount;
            CategoryId = categoryId;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
        }
    }
}