using BOL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class CustomerDB : DataModelDB<Customer>
    {
        /// <summary>
        /// Delete an Existing Customer by ID and related Call Notes
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(int? id)
        {
            Customer customer = db.Customers.Find(id);
            // Delete related call notes
            IEnumerable<CallNote> callNotes = db.CallNotes.Where(c => c.customer_id == id);
            if (callNotes != null)
            {
                foreach (var callNote in callNotes)
                {
                    db.CallNotes.Remove(callNote);
                }
            }
            // Delete customer
            db.Customers.Remove(customer);
            db.SaveChanges();
        }
        public int CountSameUsername(string username)
        {
            return db.Customers.Where(c => c.username.Equals(username)).Count();
        }
    }
}
