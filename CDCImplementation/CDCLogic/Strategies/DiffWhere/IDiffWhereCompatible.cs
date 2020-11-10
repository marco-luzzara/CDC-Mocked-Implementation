using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CDCImplementation.CDCLogic.Strategies.DiffWhere
{
    public interface IDiffWhereCompatible
    {
        string GetKeyHash(HashAlgorithm hashAlgorithm);
        string GetNonKeyHash(HashAlgorithm hashAlgorithm);
    }
}
