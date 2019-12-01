using BOL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CallNoteDB : DataModelDB<CallNote>
    {
        /// <summary>
        /// Delete the Call Note with id and all children of it
        /// </summary>
        /// <param name="id"></param>
        public override void Delete(int? id)
        {
            DeleteChildren(id);
            db.SaveChanges();
        }
        /// <summary>
        /// Get all call notes under the given customer (id)
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<CallNote> GetByCustomerId(int? customerId)
        {
            var callNotes = db.CallNotes.Where(c => c.customer_id == customerId);
            return callNotes;
        }
        /// <summary>
        /// Delete node and it's children recursively
        /// </summary>
        /// <param name="id"></param>
        public void DeleteChildren(int? id)
        {
            CallNote parent = db.CallNotes.Find(id);
            IEnumerable<CallNote> children = db.CallNotes.Where(c => c.parent_id == id);
            if (children != null)
            {
                foreach (var child in children)
                {
                    DeleteChildren(child.id);
                }
            }
            db.CallNotes.Remove(parent);
        }
        public IEnumerable<CallNote> GetChildrenById(int? id)
        {
            CallNote parent = db.CallNotes.Find(id);
            return db.CallNotes.Where(c => c.parent_id == id);
        }
        /// <summary>
        /// Initiallise Call Note when create a new note
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public CallNote InitialiseCallNote(int? customerId, int? parentId)
        {
            var callNote = new CallNote
            {
                customer_id = (int)customerId,
                parent_id = parentId,
                Customer = db.Customers.Find(customerId)
            };
            return callNote;
        }
    }
}
