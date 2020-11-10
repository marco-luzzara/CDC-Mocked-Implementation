using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CDCImplementation.Infrastructure.Hashing
{
    public class HashUtils
    {
        public static string ComputeAggregationHash(HashAlgorithm hashAlgorithm, params object[] values)
        {
            if (values.Length == 1)
                return values[0].ToString().ComputeHash(hashAlgorithm);
            else
            {
                var sb = new StringBuilder();
                foreach (var val in values)
                    sb.Append(val.ToString().ComputeHash(hashAlgorithm));

                return sb.ToString().ComputeHash(hashAlgorithm);
            }
        }
    }
}
