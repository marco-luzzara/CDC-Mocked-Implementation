using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataGenerator
{
    public interface IGenerator<T> : IGenerator
    {
        new T Generate();
    }

    public interface IGenerator
    {
        object Generate();
    }
}
