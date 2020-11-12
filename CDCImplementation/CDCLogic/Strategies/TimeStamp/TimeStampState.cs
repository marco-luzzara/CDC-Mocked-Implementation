using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.TimeStamp
{
    [DataContract]
    public class TimeStampState
    {
        [DataMember(Name = "lastUpdate")]
        public DateTimeOffset LastUpdate { get; }

        public TimeStampState(DateTimeOffset lastUpdate)
        {
            this.LastUpdate = lastUpdate;
        }
    }
}
