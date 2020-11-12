using CDCImplementation.DataGenerator.BuiltInGenerators.Traits;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace CDCImplementation.DataGenerator.BuiltInGenerators
{
    public class IntGenerator : AbstractGenerator<int>, IUniqueGenerator<int>
    {
        protected Random random = null;
        protected int minNumber;
        protected int maxNumber;

        protected List<int> uniques = null;
        public List<int> Uniques => this.uniques;

        protected int uniqueSequenceLen;
        public int UniqueSequenceLen => this.uniqueSequenceLen;

        protected IntGenerator() : base()
        {
        }

        protected int InternalGenerate()
        {
            return random.Next(minNumber, maxNumber);
        }

        protected override int BuiltInGenerate()
        {
            if (this.uniqueSequenceLen > 1)
                return ((IUniqueGenerator<int>)this).GetUnique(InternalGenerate);

            return InternalGenerate();
        }

        public class IntGeneratorBuilder : AbstractGeneratorBuilder
        {
            protected int minNumber = int.MinValue;
            protected int maxNumber = int.MaxValue;
            protected int uniqueSequenceLen = 0;

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

            public IntGeneratorBuilder Unique(int uniqueSequenceLen)
            {
                this.uniqueSequenceLen = uniqueSequenceLen;
                return this;
            }

            public override AbstractGenerator<int> Build()
            {
                var generator = (IntGenerator) InternalBuild();
                generator.minNumber = this.minNumber;
                generator.maxNumber = this.maxNumber;
                generator.random = new Random();
                generator.uniqueSequenceLen = this.uniqueSequenceLen;
                generator.uniques = new List<int>();

                return generator;
            }

            protected override AbstractGenerator<int> InternalBuild() => new IntGenerator();
        }
    }
}
