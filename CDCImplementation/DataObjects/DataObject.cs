using CDCImplementation.CDCLogic.Strategies.DiffWhere;
using CDCImplementation.CDCLogic.Strategies.TimeStamp;
using CDCImplementation.Infrastructure.Hashing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CDCImplementation.DataObjects
{
    public class DataObject : IDiffWhereCompatible, ITimeStampCompatible
    {
        [Key]
        public int FirstId { get; set; }

        [Key]
        public int SecondId { get; set; }

        public string Value { get; set; }

        public DateTimeOffset CreationTime { get; set; }

        public DateTimeOffset LastChangeTime { get; set; }


        public string GetKeyHash(HashAlgorithm hashAlgorithm)
        {
            return HashUtils.ComputeAggregationHash(hashAlgorithm, this.FirstId, this.SecondId);
        }

        public string GetNonKeyHash(HashAlgorithm hashAlgorithm)
        {
            return HashUtils.ComputeAggregationHash(hashAlgorithm, this.Value, this.CreationTime, this.LastChangeTime);
        }

        public DateTimeOffset GetTimestamp()
        {
            return this.LastChangeTime;
        }
    }
}
