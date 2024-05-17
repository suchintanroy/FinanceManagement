using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.IFinaceRepository.Exceptions
{
    public class ExpenseNotFoundException : ApplicationException
    {

        public ExpenseNotFoundException(string message) : base(message) { }

    }
}
