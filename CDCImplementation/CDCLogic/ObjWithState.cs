using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.CDCLogic
{
    public class ObjWithState<TObject>
    {
        public TObject WrappedObject { get; }

        public ObjectState State { get; }

        public ObjWithState(TObject wrappedObject, ObjectState state)
        {
            this.WrappedObject = wrappedObject;
            this.State = state;
        }
    }
}
