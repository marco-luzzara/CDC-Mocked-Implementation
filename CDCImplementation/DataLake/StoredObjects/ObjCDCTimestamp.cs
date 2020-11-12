using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CDCImplementation.DataLake.StoredObjects
{
    [DataContract]
    public class ObjCDCTimestamp<T> : ObjWithState<T>
    {
        [IgnoreDataMember]
        public override ObjectState State => base.State;

        public ObjCDCTimestamp(T wrappedObject, ObjectState state, DateTimeOffset creationTime) 
            : base(wrappedObject, state, creationTime)
        {
        }
    }
}
