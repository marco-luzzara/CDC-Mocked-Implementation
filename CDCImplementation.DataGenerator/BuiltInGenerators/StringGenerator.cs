using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDCImplementation.DataGenerator.BuiltInGenerators
{
    public class StringGenerator : IGenerator<string>
    {
        protected Random random = null;
        protected int maxLen;
        protected Func<string> prefix;
        protected Func<string> customGenerator = null;
        protected string charPool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";

        protected StringGenerator()
        {
        }

        public string Generate()
        {
            if (this.customGenerator != null)
                return customGenerator();

            var genString = string.Concat(Enumerable.Range(0, this.maxLen)
                .Select(x => this.charPool[this.random.Next(charPool.Length)]));
            return prefix() + genString;
        }

        object IGenerator.Generate()
        {
            return Generate();
        }

        public class StringGeneratorBuilder
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

            public StringGenerator Build(Func<string> customGenerator)
            {
                var generator = InternalBuild();
                generator.customGenerator = customGenerator;

                return generator;
            }

            public StringGenerator Build()
            {
                var generator = InternalBuild();
                generator.maxLen = this.maxLen;
                generator.prefix = this.prefix;
                generator.random = new Random();

                return generator;
            }

            protected StringGenerator InternalBuild()
            {
                return new StringGenerator();
            }
        }
    }
}
