using System;
namespace FinanceManagement
{
  internal  class Program 
    {


        static void Main(String[] args)
        {
            FinanceApp financeApp = new FinanceApp();
            financeApp.LoginOrCreateUser();
            financeApp.DisplayMainMenu();
          
       }
    }
}