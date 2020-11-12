using CDCImplementation.CDCLogic;
using CDCImplementation.CDCLogic.Strategies;
using CDCImplementation.CDCLogic.Strategies.DiffWhere;
using CDCImplementation.CDCLogic.Strategies.TimeStamp;
using CDCImplementation.DataGenerator;
using CDCImplementation.DataGenerator.BuiltInGenerators;
using CDCImplementation.DataLake;
using CDCImplementation.DataObjects;
using CDCImplementation.DataRetrieval;
using System;

namespace CDCImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringGenerator = new StringGenerator.StringGeneratorBuilder().SetMaximumLength(5).Build();
            var dataGeneratorForPartition1 = (ComplexObjectGenerator<DataObject>)new ComplexObjectGenerator<DataObject>.ComplexObjectGeneratorBuilder()
                .SetupPropertyGenerator(o => o.FirstId, new IntGenerator.IntGeneratorBuilder().SetMin(1).SetMax(1000).Build())
                .SetupPropertyGenerator(o => o.Value, stringGenerator)
                .Build();

            var dataGeneratorForPartition2 = (ComplexObjectGenerator<DataObject>)new ComplexObjectGenerator<DataObject>.ComplexObjectGeneratorBuilder()
                .SetupPropertyGenerator(o => o.FirstId, new IntGenerator.IntGeneratorBuilder().SetMin(1001).SetMax(2000).Build())
                .SetupPropertyGenerator(o => o.Value, stringGenerator)
                .Build();

            var dataStorage1 = new MockDataStorage<DataObject>(10, dataGeneratorForPartition1);
            var dataStorage2 = new MockDataStorage<DataObject>(10, dataGeneratorForPartition2);

            ICDCStrategy<TimeStampState> timestampStrategy = new CDCStrategyByTimestamp();
            ICDCStrategy<DiffWhereState> diffWhereStrategy = new CDCStrategyByDiffWhere();

            AbstractDataLake dataLake = new MockDataLake("DataLake");

            var diffWhereContext = new CDCRunnerContext<DataObject, DiffWhereState>(new CDCRunnerContext<DataObject, DiffWhereState>.CDCRunnerContextConfig()
            {
                DataSourceId = "dataobject_timestamp",
                CdcStrategy = diffWhereStrategy,
                DataLake = dataLake,
                DataSourcePartitions = new IObjectStorage<DataObject>[] { dataStorage1, dataStorage2 }
            });

            for (int i = 0; i < 10; i++)
            {
                diffWhereContext.StartCDC();
            }
        }
    }
}
