using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.TimeStamp
{
    public class CDCStrategyByTimestamp : ICDCStrategy<TimeStampState>
    {
        public (IEnumerable<ObjWithState<TObject>>, TimeStampState) GetFreshRows<TObject>(IEnumerable<TObject> rows, TimeStampState currentState)
        {
            var timestampedRows = rows.Cast<ITimeStampCompatible>();
            var now = DateTimeOffset.Now;

            var newState = new TimeStampState(now);
            var freshRows = timestampedRows
                .Where(x => x.GetTimestamp() > newState.LastUpdate)
                .Select(x => new ObjWithState<ITimeStampCompatible>(x, ObjectState.Inserted));

            return (freshRows.Cast<ObjWithState<TObject>>(), newState);
        }
    }
}
