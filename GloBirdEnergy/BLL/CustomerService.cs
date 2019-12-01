using BOL;
using DAL;
using NLog;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class CustomerService : Service<Customer>
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public readonly NameSearcher searcher;
        public readonly CustomerValidator customerValidator; 
        public CustomerService()
        {
            dataModelDB = new CustomerDB();
            searcher = new NameSearcher();
            customerValidator = new CustomerValidator();
        }
        public override void Insert(Customer customer)
        {
            try
            {
                customer.id = idCreator.CreateId();
                base.Insert(customer);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error,ex.Message);
                throw ex;
            }
        }
        public IEnumerable<Customer> Search(string searchString)
        {
            try
            {
                var customers = GetAll();
                return searcher.Search(customers, searchString);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// Validation for username unique
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool CheckUsernameUnique(string username)
        {
            try
            {
                var sameUsernameCount = 0;
                if (!String.IsNullOrEmpty(username))
                {
                    sameUsernameCount = ((CustomerDB)dataModelDB).CountSameUsername(username);
                }
                if (sameUsernameCount > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
}
    }
}
