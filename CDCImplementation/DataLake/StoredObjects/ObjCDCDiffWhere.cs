using CDCImplementation.CDCLogic;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CDCImplementation.DataLake.StoredObjects
{
    [DataContract]
    public class ObjCDCDiffWhere<T> : ObjWithState<T>
    {
        [DataMember(Name = "khash")]
        public string KeyHash { get; set; }

        [DataMember(Name = "hash")]
        public string NonKeyHash { get; set; }

        public ObjCDCDiffWhere(T wrappedObject, ObjectState state) : base(wrappedObject, state)
        {
        }
    }
}
