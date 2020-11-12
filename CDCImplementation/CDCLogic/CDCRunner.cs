using CDCImplementation.CDCLogic.Strategies;
using CDCImplementation.DataLake;
using CDCImplementation.DataRetrieval;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.CDCLogic
{
    public class CDCRunner
    {
        public CDCRunner()
        {
        }

        public void StartCDC<TObject, TState>(CDCConfig<TObject, TState> cdcConfig)
            where TState : class
            where TObject : class
        {
            var currentState = cdcConfig.DataLake.GetCurrentState<TState>(cdcConfig.SourceId, cdcConfig.PartitionId);
            var rows = cdcConfig.DataSource.GetData();
            var (freshRows, newState) = cdcConfig.CdcStrategy.GetFreshRows(rows, currentState);

            cdcConfig.DataLake.InsertFreshRows(freshRows, cdcConfig.SourceId, cdcConfig.PartitionId);
            cdcConfig.DataLake.SetCurrentState(newState, cdcConfig.SourceId, cdcConfig.PartitionId);
        }

        public class CDCConfig<TObject, TState>
        {
            public AbstractDataLake DataLake { get; set; }
            public ICDCStrategy<TState> CdcStrategy { get; set; }
            public IObjectStorage<TObject> DataSource { get; set; }
            public string SourceId { get; set; }
            public string PartitionId { get; set; }
        }
    }
}
