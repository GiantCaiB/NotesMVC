using BLL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class IntIdCreator : IIdCreator<int>
    {
        /// <summary>
        /// Create an integer random ID
        /// </summary>
        /// <returns></returns>
        public int CreateId()
        {
            int id = 0;
            var random = new Random(Guid.NewGuid().GetHashCode());
            id = random.Next();
            return id;
        }
    }
}
