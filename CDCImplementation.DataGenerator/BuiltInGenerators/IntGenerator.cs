using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CDCImplementation.DataGenerator.BuiltInGenerators
{
    public class IntGenerator : IGenerator<int>
    {
        protected Random random = null;
        protected int minNumber;
        protected int maxNumber;
        protected Func<int> customGenerator = null;

        protected IntGenerator()
        {
        }

        public int Generate()
        {
            if (this.customGenerator != null)
                return customGenerator();

            return random.Next(minNumber, maxNumber);
        }

        object IGenerator.Generate()
        {
            return Generate();
        }

        public class IntGeneratorBuilder
        {
            protected int minNumber = int.MinValue;
            protected int maxNumber = int.MaxValue;

            public IntGeneratorBuilder SetUpperBound(int maxNumber)
            {
                this.maxNumber = maxNumber;
                return this;
            }

            public IntGeneratorBuilder SetLowerBound(int minNumber)
            {
                this.minNumber = minNumber;
                return this;
            }

            public IntGenerator Build(Func<int> customGenerator)
            {
                var generator = InternalBuild();
                generator.customGenerator = customGenerator;

                return generator;
            }

            public IntGenerator Build()
            {
                var generator = InternalBuild();
                generator.minNumber = this.minNumber;
                generator.maxNumber = this.maxNumber;
                generator.random = new Random();

                return generator;
            }

            protected IntGenerator InternalBuild()
            {
                return new IntGenerator();
            }
        }
    }
}
