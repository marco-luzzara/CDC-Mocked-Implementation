using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataLake
{
    public abstract class AbstractDataLake<TState, TObject>
    {
        public abstract TState GetCurrentState();

        public abstract void SetCurrentState(TState newState);

        public abstract void InsertFreshRows(IEnumerable<ObjWithState<TObject>> freshRows);

        // TODO: change to concrete, see stream
        public abstract IEnumerable<TObject> ReadAll();
    }
}
