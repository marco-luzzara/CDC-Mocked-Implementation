using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.TimeStamp
{
    [Serializable]
    public class TimeStampState : ISerializable
    {
        public DateTimeOffset LastUpdate { get; }

        public TimeStampState(DateTimeOffset lastUpdate)
        {
            this.LastUpdate = lastUpdate;
        }

        protected TimeStampState(SerializationInfo info, StreamingContext context)
        {
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
