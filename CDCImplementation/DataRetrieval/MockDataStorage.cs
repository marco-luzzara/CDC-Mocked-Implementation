using CDCImplementation.DataGenerator;
using CDCImplementation.DataGenerator.BuiltInGenerators;
using CDCImplementation.DataObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataRetrieval
{
    public class MockDataStorage<T> : IObjectStorage<T> where T : new()
    {
        protected readonly ComplexObjectGenerator<T> dataGenerator;
        protected readonly int objectsReturned;

        public MockDataStorage(int objectsReturned, ComplexObjectGenerator<T> dataGenerator)
        {
            this.dataGenerator = dataGenerator;
            this.objectsReturned = objectsReturned;
        }

        public IEnumerable<T> GetData()
        {
            for (int i = 0; i < this.objectsReturned; i++)
            {
                yield return this.dataGenerator.Generate();
            }
        }
    }
}
