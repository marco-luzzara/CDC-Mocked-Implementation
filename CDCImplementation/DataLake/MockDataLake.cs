using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataLake
{
    public class MockDataLake<TObject, TState> : AbstractDataLake<TObject, TState>
    {
        public override void CommitCDC()
        {
            throw new NotImplementedException();
        }

        public override TState GetCurrentState()
        {
            throw new NotImplementedException();
        }

        public override void InitializeCDC()
        {
            throw new NotImplementedException();
        }

        public override void InsertFreshRows(IEnumerable<ObjWithState<TObject>> freshRows, string sourceId, string runnerId)
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
