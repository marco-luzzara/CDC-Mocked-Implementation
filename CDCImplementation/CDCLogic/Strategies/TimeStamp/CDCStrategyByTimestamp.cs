using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.TimeStamp
{
    public class CDCStrategyByTimestamp : ICDCStrategy<TimeStampState>
    {
        public (IEnumerable<ObjWithState<TObject>>, TimeStampState) GetFreshRows<TObject>(IEnumerable<TObject> rows, TimeStampState currentState)
        {
            throw new NotImplementedException();
        }
    }
}
