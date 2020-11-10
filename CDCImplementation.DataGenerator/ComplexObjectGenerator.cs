using CDCImplementation.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace CDCImplementation.DataGenerator
{
    public class ComplexObjectGenerator<T> : IGenerator<T>
        where T : new()
    {
        protected Dictionary<PropertyInfo, IGenerator> generatorMapper;
        protected Func<T> customGenerator = null;

        public T Generate()
        {
            //var newObj = new T();
            throw new NotImplementedException();
        }

        object IGenerator.Generate()
        {
            return Generate();
        }

        public class ComplexObjectGeneratorBuilder<TBuilder> where TBuilder : T
        {
            protected Dictionary<PropertyInfo, IGenerator> generatorMapper = new Dictionary<PropertyInfo, IGenerator>();

            public ComplexObjectGeneratorBuilder<TBuilder> SetupPropertyGenerator(Expression<Func<T, object>> propertyAccess, IGenerator propertyGenerator)
            {
                var propInfo = ReflectionUtils.GetPropertyInfo(propertyAccess);
                this.generatorMapper[propInfo] = propertyGenerator;

                return this;
            }

            public ComplexObjectGenerator<T> Build(Func<T> customGenerator)
            {
                var generator = InternalBuild();
                generator.customGenerator = customGenerator;

                return generator;
            }

            public ComplexObjectGenerator<T> Build()
            {
                var generator = InternalBuild();
                generator.generatorMapper = this.generatorMapper;
                
                return generator;
            }

            protected ComplexObjectGenerator<T> InternalBuild()
            {
                return new ComplexObjectGenerator<T>();
            }
        }
    }
}
