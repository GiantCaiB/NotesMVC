using BLL.Contract;
using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class NameSearcher : ISearchable<Customer>
    {
        /// <summary>
        ///  Search of customers by First Name or Last Name
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="searchString"></param>
        /// <returns></returns>
        public IEnumerable<Customer> Search(IEnumerable<Customer> customers, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => (s.first_name.ToUpper().Contains(searchString.ToUpper()) || s.last_name.ToUpper().Contains(searchString.ToUpper())));
            }
            return customers;
        }
    }
}
