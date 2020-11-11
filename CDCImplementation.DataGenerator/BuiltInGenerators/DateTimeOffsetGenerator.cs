using System;
using System.Collections.Generic;
using System.Text;

namespace CDCImplementation.DataGenerator.BuiltInGenerators
{
    public class DateTimeOffsetGenerator : AbstractGenerator<DateTimeOffset>
    {
        protected Random random = null;
        protected (long, long) ticksRange;

        // https://stackoverflow.com/questions/6651554/random-number-in-long-range-is-this-the-way
        protected long LongRandom(long min, long max, Random rand)
        {
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min);
        }

        protected override DateTimeOffset BuiltInGenerate()
        {
            var genTicks = LongRandom(this.ticksRange.Item1, this.ticksRange.Item2, this.random);
            var dto = new DateTimeOffset(genTicks, TimeSpan.Zero);

            return dto;
        }

        public class DateTimeOffsetGeneratorBuilder : AbstractGeneratorBuilder
        {
            protected (int, int) yearRange = (DateTimeOffset.MinValue.Year, DateTimeOffset.MaxValue.Year);

            // [minYear, maxYear[
            public DateTimeOffsetGeneratorBuilder SetYearRange(int minYear, int maxYear)
            {
                if (minYear > maxYear || minYear < DateTimeOffset.MinValue.Year || maxYear > DateTimeOffset.MaxValue.Year)
                    throw new ArgumentOutOfRangeException("minYear must be less than maxYear and they both be in the range [DateTimeOffset.MinValue.Year, DateTimeOffset.MaxValue.Year[");

                yearRange = (minYear, maxYear);

                return this;
            }

            public override AbstractGenerator<DateTimeOffset> Build()
            {
                var generator = (DateTimeOffsetGenerator) InternalBuild();
                var minTick = new DateTimeOffset(this.yearRange.Item1, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks;
                var maxTick = new DateTimeOffset(this.yearRange.Item2, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks;

                generator.random = new Random();
                generator.ticksRange = (minTick, maxTick);

                return generator;
            }

            protected override AbstractGenerator<DateTimeOffset> InternalBuild() => new DateTimeOffsetGenerator();
        }
    }
}
