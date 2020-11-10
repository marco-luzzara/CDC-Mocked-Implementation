using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.TimeStamp
{
    public interface ITimeStampCompatible
    {
        DateTimeOffset GetTimestamp();
    }
}
