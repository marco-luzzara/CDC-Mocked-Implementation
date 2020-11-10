using CDCImplementation.CDCLogic.Strategies;
using CDCImplementation.DataLake;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.CDCLogic
{
    public class CDCRunner<TObject, TState>
    {
        protected AbstractDataLake<TObject, TState> dataLake;
        protected ICDCStrategy<TState> cdcStrategy;

        public CDCRunner(AbstractDataLake<TObject, TState> dataLake, ICDCStrategy<TState> cdcStrategy)
        {
            this.dataLake = dataLake;
            this.cdcStrategy = cdcStrategy;
        }

        public void StartCDC(IEnumerable<TObject> rows, string sourceId)
        {
            var currentState = this.dataLake.GetCurrentState();
            var (freshRows, newState) = cdcStrategy.GetFreshRows(rows, currentState);

            this.dataLake.InsertFreshRowsAndUpdateState(freshRows, newState, sourceId);
        }
    }
}
