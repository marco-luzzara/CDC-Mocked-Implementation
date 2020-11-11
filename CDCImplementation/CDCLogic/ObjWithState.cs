using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CDCImplementation.CDCLogic
{
    [DataContract]
    public class ObjWithState<TObject>
    {
        [DataMember(Name = "object")]
        public virtual TObject WrappedObject { get; }

        [DataMember(Name = "state")]
        public virtual ObjectState State { get; }

        [DataMember(Name = "ts_load")]
        public virtual DateTimeOffset CreationTime { get; set; }

        public ObjWithState(TObject wrappedObject, ObjectState state)
        {
            this.WrappedObject = wrappedObject;
            this.State = state;
        }
    }

    public enum ObjectState
    {
        Inserted,
        Updated,
        Deleted
    }
}
