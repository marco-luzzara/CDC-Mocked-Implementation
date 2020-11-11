using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataLake
{
    public abstract class AbstractDataLake<TObject, TState>
    {
        // for move and rename pattern
        public abstract void InitializeCDC();

        public abstract void CommitCDC();

        public abstract TState GetCurrentState();

        public abstract void SetCurrentState(TState newState);

        public abstract void InsertFreshRows(IEnumerable<ObjWithState<TObject>> freshRows, string sourceId, string runnerId);

        // TODO: change to concrete, see stream
        public abstract IEnumerable<TObject> ReadAll();
    }
}
