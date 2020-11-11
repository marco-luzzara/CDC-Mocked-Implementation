using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataGenerator
{
    public abstract class AbstractGenerator<T> : IGenerator<T>
    {
        protected Func<T> generator = null;

        protected AbstractGenerator() 
        {
        }

        protected abstract T BuiltInGenerate();

        public virtual T Generate()
        {
            if (this.generator != null)
                return generator();

            return BuiltInGenerate();
        }

        object IGenerator.Generate()
        {
            return Generate();
        }

        public abstract class AbstractGeneratorBuilder
        {
            public virtual AbstractGenerator<T> Build(Func<T> customGenerator)
            {
                var generator = InternalBuild();
                generator.generator = customGenerator;

                return generator;
            }

            public abstract AbstractGenerator<T> Build();

            protected abstract AbstractGenerator<T> InternalBuild();
        }
    }
}
