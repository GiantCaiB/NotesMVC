using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI.ViewModels
{
    public class CustomerIndexData
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<CallNote> CallNotes { get; set; }
    }
}