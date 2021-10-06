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

            object result = null; 
            foreach (var constructorInfo in orderedConstructors)
            {
                var parametersInfo = constructorInfo.GetParameters();
                var parameters = new List<object>();
                foreach (var parameter in parametersInfo)
                {
                    parameters.Add(Create(parameter.ParameterType));
                }
                result = constructorInfo.Invoke(parameters.ToArray());
            }

            if (result == null) 
                return CreateDefaultValue(targetType);
            
            FillPublicFields(result, targetType, generatorContext);
            FillPublicProperties(result, targetType, generatorContext);

            return result;
        }

        private void FillPublicFields(object targetObject, Type targetType, GeneratorContext generatorContext)
        {
            var fields = targetType.GetFields();

            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.IsPublic && fieldInfo.GetValue(targetObject) == CreateDefaultValue(targetType))
                {
                    fieldInfo.SetValue(targetObject, Create(fieldInfo.FieldType));
                }
            }
        }
        
        private void FillPublicProperties(object targetObject, Type targetType, GeneratorContext generatorContext)
        {
            var properties = targetType.GetProperties();

            foreach (var property in properties)
            {
                if (property.CanWrite && property.GetValue(targetObject) == CreateDefaultValue(targetType))
                {
                    property.SetValue(targetObject, Create(property.PropertyType));
                }
            }
        }
        
        private static object CreateDefaultValue(Type t)
        {
            return t.IsValueType 
                ? Activator.CreateInstance(t) 
                : null;
        }
        
    }
}