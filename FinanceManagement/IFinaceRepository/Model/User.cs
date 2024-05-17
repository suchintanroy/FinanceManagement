using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.IFinaceRepository.Model
{
    public class User
    { 
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }

        public User() { }

        public User(int userId, string username, string password, string email, string contactNumber)
        {
            UserId = userId;
            Username = username;
            Password = password;
            Email = email;
            ContactNumber = contactNumber;
        }


    }
}

  