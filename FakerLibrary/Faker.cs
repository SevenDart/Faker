using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using FakerLibrary.Generators;

namespace FakerLibrary
{
    public class Faker : IFaker
    {
        private readonly Dictionary<Type, IValueGenerator> _generators;

        public Faker()
        {
            _generators = new Dictionary<Type, IValueGenerator>
            {
                {typeof(int), new IntegerGenerator()},
                {typeof(bool), new BooleanGenerator()},
                {typeof(double), new DoubleGenerator()}
            };
        }

        public T Create<T>()
        {
            return (T) Create(typeof(T));
        }

        private object Create(Type targetType)
        {
            var generatorContext = new GeneratorContext(new Random(), targetType, this);

            _generators.TryGetValue(targetType, out var generator);
            if (generator != null)
            {
                return generator.Generate(generatorContext);
            }

            var constructors = targetType.GetConstructors();
            var orderedConstructors = constructors.OrderByDescending(c => c.GetParameters().Length);

            foreach (var constructorInfo in orderedConstructors)
            {
                var parametersInfo = constructorInfo.GetParameters();
                var parameters = new List<object>();
                foreach (var parameter in parametersInfo)
                {
                    parameters.Add(Create(parameter.ParameterType));
                }
                var result = constructorInfo.Invoke(parameters.ToArray());
                return result;
            }
            
            

            return CreateDefaultValue(targetType);
        } 

        private static object CreateDefaultValue(Type t)
        {
            return t.IsValueType 
                ? Activator.CreateInstance(t) 
                : null;
        }
        
    }
}