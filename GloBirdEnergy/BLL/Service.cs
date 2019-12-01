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
    public class Service<T> : IService<T> where T : class
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public DataModelDB<T> dataModelDB;
        public readonly IntIdCreator idCreator;
        public Service()
        {
            dataModelDB = new DataModelDB<T>();
            idCreator = new IntIdCreator();
        }
        public void SetDB(DataModelDB<T> dataModelDB)
        {
            this.dataModelDB = dataModelDB;
        }
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return dataModelDB.GetAll();
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        public virtual T GetById(int? id)
        {
            try
            {
                return dataModelDB.GetById(id);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        public virtual void Insert(T dataModel)
        {
            try
            {
                if (dataModel != null)
                {
                    dataModelDB.GetById(dataModel);
                }
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        public virtual void Update(T datamodel)
        {
            try
            {
                dataModelDB.Update(datamodel);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        public virtual void Delete(int? id)
        {
            try
            {
                dataModelDB.Delete(id);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex.Message);
                throw ex;
            }
        }
        public virtual void Dispose(bool disposing)
        {
            dataModelDB.Dispose(disposing);
        }
        public virtual int CreateId()
        {
            int id;
            do
            {
                id = idCreator.CreateId();
            }
            while (GetById(id) != null);
            return id;
        }
    }
}
