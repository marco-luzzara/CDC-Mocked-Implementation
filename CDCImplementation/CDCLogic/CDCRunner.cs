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

        public TState StartCDC<TObject, TState>(
            TState currentState,
            AbstractDataLake<TObject, TState> dataLake,
            ICDCStrategy<TState> cdcStrategy, 
            IObjectStorage<TObject> dataSource, 
            string sourceId,
            string runnerId)
        {
            var rows = dataSource.GetData();
            var (freshRows, newState) = cdcStrategy.GetFreshRows(rows, currentState);

            dataLake.InsertFreshRows(freshRows, sourceId, runnerId);
            return newState;
        }
    }
}
