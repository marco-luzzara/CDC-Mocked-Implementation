using CDCImplementation.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataRetrieval
{
    public class MockDataStorage : IObjectStorage<DataObject>
    {
        public IEnumerable<DataObject> GetData()
        {
            throw new NotImplementedException();
        }
    }
}
