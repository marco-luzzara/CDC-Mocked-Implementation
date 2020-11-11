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
        {
            var timestampedRows = rows.Cast<ITimeStampCompatible>();

            var newState = new TimeStampState(DateTimeOffset.Now);
            var freshRows = timestampedRows
                .Where(x => x.GetTimestamp() > currentState.LastUpdate)
                .Select(x => new ObjCDCTimestamp<ITimeStampCompatible>(x, ObjectState.Inserted)
                {
                    CreationTime = newState.LastUpdate
                });

            return (freshRows.Cast<ObjWithState<TObject>>(), newState);
        }

        public TimeStampState JoinPartialStates(params TimeStampState[] partialStates)
        {
            // find the state with the minimum timestamp. 
            // In this way you are sure that you are not leaving out some rows on the next CDC cycle
            return partialStates.Aggregate((min, cur) => cur.LastUpdate < min.LastUpdate ? cur : min);
        }
    }
}
