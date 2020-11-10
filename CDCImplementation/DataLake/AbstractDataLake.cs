using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataLake
{
    public abstract class AbstractDataLake<TObject, TState>
    {
        public abstract TState GetCurrentState();

        public abstract void SetCurrentState(TState newState);

        public abstract void InsertFreshRowsAndUpdateState(IEnumerable<ObjWithState<TObject>> freshRows, TState newState, string sourceId);

        // TODO: change to concrete, see stream
        public abstract IEnumerable<TObject> ReadAll();
    }
}
