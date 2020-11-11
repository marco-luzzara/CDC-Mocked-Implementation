using CDCImplementation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CDCImplementation.DataGenerator
{
    public class ComplexObjectGenerator<T> : AbstractGenerator<T>
        where T : new()
    {
        protected Dictionary<PropertyInfo, IGenerator> generatorMapper;

        protected ComplexObjectGenerator() : base()
        {
        }

        protected override T BuiltInGenerate()
        {
            var newObj = new T();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (this.generatorMapper.ContainsKey(prop))
                {
                    var genValue = this.generatorMapper[prop].Generate();
                    prop.SetValue(newObj, genValue);
                }
            }

            return newObj;
        }

        public class ComplexObjectGeneratorBuilder : AbstractGeneratorBuilder
        {
            protected Dictionary<PropertyInfo, IGenerator> generatorMapper = new Dictionary<PropertyInfo, IGenerator>();

            public ComplexObjectGeneratorBuilder SetupPropertyGenerator(Expression<Func<T, object>> propertyAccess, IGenerator propertyGenerator)
            {
                var propInfo = ReflectionUtils.GetPropertyInfo(propertyAccess);
                this.generatorMapper[propInfo] = propertyGenerator;

                return this;
            }

            public override AbstractGenerator<T> Build()
            {
                var generator = (ComplexObjectGenerator<T>) InternalBuild();
                generator.generatorMapper = this.generatorMapper;
                
                return generator;
            }

            protected override AbstractGenerator<T> InternalBuild() => new ComplexObjectGenerator<T>();
        }
    }
}
