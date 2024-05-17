using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManagement.IFinaceRepository.Model;

namespace FinanceManagement.IFinaceRepository.Repository
{
   public interface IUserService
    {

        public int CreateUser(User user);
        public bool DeleteUser(int userId);


    }
}
