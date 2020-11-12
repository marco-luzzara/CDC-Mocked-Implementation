using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataLake
{
    public abstract class AbstractDataLake
    {
        // for move and rename pattern
        public abstract void InitializeCDC(string sourceId);

        public abstract void CommitCDC(string sourceId);

        public abstract TState GetCurrentState<TState>(string sourceId, string partitionId) where TState : class;

        public abstract void SetCurrentState<TState>(TState newState, string sourceId, string partitionId);

        public abstract void InsertFreshRows<TObject>(IEnumerable<ObjWithState<TObject>> freshRows, string sourceId, string partitionId);

        // TODO: change to concrete, see stream
        public abstract IEnumerable<TObject> ReadAll<TObject>(string sourceId);
    }
}
