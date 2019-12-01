using BLL.Contract;
using BOL;
using DAL;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CallNoteService : Service<CallNote>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public CallNoteService()
        {
            dataModelDB = new CallNoteDB();
        }
        public override void Insert(CallNote callNote)
        {
            try
            {
                callNote.date_created = DateTime.Now;
                base.Insert(callNote);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// Update Call Note and prevent changing the customer
        /// </summary>
        /// <param name="callNote"></param>
        public override void Update(CallNote callNote)
        {
            try
            {
                callNote.Customer = (GetById(callNote.id)).Customer;
                callNote.customer_id = callNote.Customer.id;
                base.Update(callNote);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// Get all Call Notes under gvien Customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IEnumerable<CallNote> GetByCustomerId(int? customerId)
        {
            try
            {
                return ((CallNoteDB)dataModelDB).GetByCustomerId(customerId);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// Create new node and set the customer id and parentId by code
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public CallNote InitialiseCallNote(int? customerId, int? parentId)
        {
            try
            {
                var callNote = ((CallNoteDB)dataModelDB).InitialiseCallNote(customerId, parentId);
                callNote.id = CreateId();
                return callNote;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
    }
}
