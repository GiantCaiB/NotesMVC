using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOL
{
    public abstract class DataModel
    {
        public int id { get; set; }
        public abstract override string ToString();
    }
}
