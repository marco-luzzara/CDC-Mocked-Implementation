using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.TimeStamp
{
    public class TimeStampState
    {
        public DateTimeOffset LastUpdate { get; }

        public TimeStampState(DateTimeOffset lastUpdate)
        {
            this.LastUpdate = lastUpdate;
        }
    }
}
