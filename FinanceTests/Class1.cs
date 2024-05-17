using NUnit.Framework;
using FinanceManagement.IFinaceRepository.Repository;
using FinanceManagement.IFinaceRepository.Service;
using FinanceManagement.IFinaceRepository.Model;
using System.Data.SqlClient;
using FinanceManagement.IFinaceRepository.Exceptions;
namespace FinanceTests
{
 
    public class Class1
    {
 
        [Test]
        public void CreateExpense_Successfully()
        {
            
            var userExpense = new UserExpense();
            var userId = 7;
            var expense = new Expense
            {
                UserId = userId,
                Amount = 100,
                CategoryId = 1,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date.AddDays(1),
                Description = "Test expense"
            };

            
            bool result = userExpense.CreateExpense(expense);

            // Assert
            Assert.That(result, "Expense creation should be successful.");
        }
        [Test]
        public void CreateUser_Successfully()
        {
            UserService userService = new UserService();

            int status = userService.CreateUser(new User
            {
                Username = "TestUser",
                Email = "test@example.com",
                Password = "password",
                ContactNumber = "1234567890"
            }
           );
            Assert.That(1, Is.EqualTo(status));

        }
    }

}
    

