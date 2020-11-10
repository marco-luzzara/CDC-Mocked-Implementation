using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.DiffWhere
{
    [Serializable]
    public class DiffWhereState : ISerializable
    {
        public Dictionary<string, string> HashedRows { get; }

        public DiffWhereState(IDictionary<string, string> hashedRows)
        {
            this.HashedRows = hashedRows.ToDictionary(x => x.Key, x => x.Value);
        }

        protected DiffWhereState(SerializationInfo info, StreamingContext context)
        {
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }
}
