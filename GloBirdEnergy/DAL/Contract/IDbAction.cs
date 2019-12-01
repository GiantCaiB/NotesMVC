using BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Contract
{
    interface IDbAction<T>
    {
        IEnumerable<T> GetAll();
        T GetById(int? id);
        void GetById(T dataModel);
        void Update(T dataModel);
        void Delete(int? id);
        void Dispose(bool disposing);
    }
}
