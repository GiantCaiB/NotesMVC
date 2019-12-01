using System.Collections.Generic;

namespace BLL.Contract
{
    interface ISearchable<T>
    {
        IEnumerable<T> Search(IEnumerable<T> target, string searchString);
    }
}
