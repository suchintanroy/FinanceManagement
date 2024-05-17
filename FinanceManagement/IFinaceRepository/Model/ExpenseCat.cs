using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagement.IFinaceRepository.Model
{
    public class ExpenseCat
    {
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public ExpenseCat() { }

        public ExpenseCat(int categoryId, string categoryName)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
        }
    }
}
