using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataLake
{
    public class MockDataLake<TObject, TState> : AbstractDataLake<TObject, TState>
    {
        public override TState GetCurrentState()
        {
            throw new NotImplementedException();
        }

        public override void InsertFreshRowsAndUpdateState(IEnumerable<ObjWithState<TObject>> freshRows, TState newState, string sourceId)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<TObject> ReadAll()
        {
            throw new NotImplementedException();
        }

        public override void SetCurrentState(TState newState)
        {
            throw new NotImplementedException();
        }
    }
}
