using BOL;
using DAL.Contract;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace DAL
{
    public class DataModelDB<T> : IDbAction<T> where T : class
    {
        public GloBirdEntities db;
        public DataModelDB()
        {
            db = new GloBirdEntities();
        }
        /// <summary>
        /// Get All DataModels
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return db.Set<T>().ToList();
        }
        /// <summary>
        /// Get Single DataModel by ID
        /// </summary>
        /// <returns></returns>
        public virtual T GetById(int? id)
        {
            return db.Set<T>().Find(id);
        }
        /// <summary>
        /// Add a New DataModel
        /// </summary>
        /// <param name="callNote"></param>
        public virtual void GetById(T dataModel)
        {
            db.Set<T>().Add(dataModel);
            db.SaveChanges();
        }
        /// <summary>
        /// Edit an Existing DataModel
        /// </summary>
        /// <param name="callNote"></param>
        public virtual void Update(T dataModel)
        {
            db.Set<T>().AddOrUpdate(dataModel);
            db.SaveChanges();
        }
        /// <summary>
        /// Delete an Existing CallNote by ID
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(int? id)
        {
            T dataModel = GetById(id);
            db.Set<T>().Remove(dataModel);
            db.SaveChanges();
        }
        /// <summary>
        /// Dispose DB
        /// </summary>
        /// <param name="disposing"></param>
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
