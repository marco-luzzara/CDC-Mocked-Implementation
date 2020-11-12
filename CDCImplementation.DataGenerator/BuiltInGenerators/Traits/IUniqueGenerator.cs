using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataGenerator.BuiltInGenerators.Traits
{
    public interface IUniqueGenerator<T>
    {
        List<T> Uniques { get; }
        int UniqueSequenceLen { get; }

        T GetUnique(Func<T> generator)
        {
            if (Uniques.Count == UniqueSequenceLen)
                Uniques.Clear();

            T genValue;
            while (Uniques.Contains(genValue = generator())) ;
            Uniques.Add(genValue);

            return genValue;
        }
    }
}
