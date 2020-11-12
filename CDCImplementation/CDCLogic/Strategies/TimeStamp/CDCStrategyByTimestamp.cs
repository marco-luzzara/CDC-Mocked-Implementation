using CDCImplementation.DataLake.StoredObjects;
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
            where TObject : class
        {
            if (currentState is null || currentState.LastUpdate == default)
                currentState = new TimeStampState(DateTimeOffset.MinValue);

            var timestampedRows = rows.Cast<ITimeStampCompatible>();

            var newState = new TimeStampState(DateTimeOffset.Now);
            var freshRows = timestampedRows
                .Where(x => x.GetTimestamp() > currentState.LastUpdate)
                .Select(x => new ObjCDCTimestamp<TObject>((TObject) x, ObjectState.Inserted, newState.LastUpdate));

            return (freshRows.Cast<ObjWithState<TObject>>(), newState);
        }
    }
}
