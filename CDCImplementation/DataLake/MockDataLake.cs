using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataLake
{
    public class MockDataLake<TState, TObject> : AbstractDataLake<TState, TObject>
    {
        public override TState GetCurrentState()
        {
            throw new NotImplementedException();
        }

        public override void InsertFreshRows(IEnumerable<ObjWithState<TObject>> freshRows)
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
