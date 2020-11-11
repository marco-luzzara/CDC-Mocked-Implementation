using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace CDCImplementation.DataGenerator.BuiltInGenerators
{
    public class IntGenerator : AbstractGenerator<int>
    {
        protected Random random = null;
        protected int minNumber;
        protected int maxNumber;

        protected IntGenerator() : base()
        {
        }

        protected override int BuiltInGenerate()
        {
            return random.Next(minNumber, maxNumber);
        }

        public class IntGeneratorBuilder : AbstractGeneratorBuilder
        {
            protected int minNumber = int.MinValue;
            protected int maxNumber = int.MaxValue;

            public IntGeneratorBuilder SetMax(int maxNumber)
            {
                this.maxNumber = maxNumber;
                return this;
            }

            public IntGeneratorBuilder SetMin(int minNumber)
            {
                this.minNumber = minNumber;
                return this;
            }

            public override AbstractGenerator<int> Build()
            {
                var generator = (IntGenerator) InternalBuild();
                generator.minNumber = this.minNumber;
                generator.maxNumber = this.maxNumber;
                generator.random = new Random();

                return generator;
            }

            protected override AbstractGenerator<int> InternalBuild() => new IntGenerator();
        }
    }
}
