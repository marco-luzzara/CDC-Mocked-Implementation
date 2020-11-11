using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDCImplementation.DataGenerator.BuiltInGenerators
{
    public class StringGenerator : AbstractGenerator<string>
    {
        protected Random random = null;
        protected int maxLen;
        protected Func<string> prefix;
        protected string charPool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";

        protected StringGenerator() : base()
        {
        }

        protected override string BuiltInGenerate()
        {
            var genString = string.Concat(Enumerable.Range(0, this.maxLen)
                .Select(x => this.charPool[this.random.Next(charPool.Length)]));
            return prefix() + genString;
        }

        public class StringGeneratorBuilder : AbstractGeneratorBuilder
        {
            protected int maxLen = 16;
            protected Func<string> prefix = () => "";

            // prefix not included
            public StringGeneratorBuilder SetMaximumLength(int maxLen)
            {
                this.maxLen = maxLen;
                return this;
            }

            public StringGeneratorBuilder SetPrefix(Func<string> prefix)
            {
                this.prefix = prefix;
                return this;
            }

            public StringGeneratorBuilder SetPrefix(string constPrefix)
            {
                this.prefix = () => constPrefix;
                return this;
            }

            public override AbstractGenerator<string> Build()
            {
                var generator = (StringGenerator) InternalBuild();
                generator.maxLen = this.maxLen;
                generator.prefix = this.prefix;
                generator.random = new Random();

                return generator;
            }

            protected override AbstractGenerator<string> InternalBuild() => new StringGenerator();
        }
    }
}
