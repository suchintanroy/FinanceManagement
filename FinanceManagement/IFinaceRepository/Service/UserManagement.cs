using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManagement.IFinaceRepository.Model;
using FinanceManagement.IFinaceRepository.Repository;

namespace FinanceManagement.IFinaceRepository.Service
{
    internal class UserManagement
    {
     // 1.User Create
        public static void CreateUser(UserService userService)
        {
            Console.WriteLine("Enter username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();

            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter contact number:");
            string contactNumber = Console.ReadLine();

            User newUser = new User
            {
                Username = username,
                Password = password,
                Email = email,
                ContactNumber = contactNumber
            };

            int userCreated = userService.CreateUser(newUser);

            if (userCreated==1)
            {
                Console.WriteLine("User created successfully!");
            }
            else
            {
                Console.WriteLine("Failed to create user. Please try again.");
            }
        }
        //2. User Delete
        public static void DeleteUser(UserService userService)
        {
            Console.WriteLine("Enter the user ID to delete:");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int userId))
            {
                bool userDeleted = userService.DeleteUser(userId);

                if (userDeleted)
                {
                    Console.WriteLine($"User with ID {userId} deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to delete user. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid user ID.");
            }
        }



    }
}
