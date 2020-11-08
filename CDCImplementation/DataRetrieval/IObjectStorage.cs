using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataRetrieval
{
    public interface IObjectStorage<T>
    {
        IEnumerable<T> GetData();
    }
}
