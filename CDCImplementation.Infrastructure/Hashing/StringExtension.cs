using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CDCImplementation.Infrastructure.Hashing
{
    public static class StringExtension
    {
        // https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithm.computehash?view=netcore-3.1
        public static string ComputeHash(this string s, HashAlgorithm hashAlg)
        {
            byte[] data = hashAlg.ComputeHash(Encoding.UTF8.GetBytes(s));

            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}