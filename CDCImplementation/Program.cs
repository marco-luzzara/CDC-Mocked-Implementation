using CDCImplementation.DataGenerator;
using CDCImplementation.DataGenerator.BuiltInGenerators;
using CDCImplementation.DataObjects;
using CDCImplementation.DataRetrieval;
using System;

namespace CDCImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            ComplexObjectGenerator<DataObject> dataGenerator = (ComplexObjectGenerator<DataObject>)new ComplexObjectGenerator<DataObject>.ComplexObjectGeneratorBuilder()
                .SetupPropertyGenerator(o => o.FirstId, new IntGenerator.IntGeneratorBuilder().SetMin(0).SetMax(20).Build())
                .Build();

            MockDataStorage<DataObject> dataStorage = new MockDataStorage<DataObject>(10, dataGenerator);

            var data = dataStorage.GetData();

            foreach (var d in data)
            {
                Console.WriteLine(d.FirstId);
            }
        }
    }
}
