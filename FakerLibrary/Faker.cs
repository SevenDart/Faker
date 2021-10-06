using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FakerLibrary.Generators;

namespace FakerLibrary
{
    public class Faker : IFaker
    {
        private readonly List<IValueGenerator> _generators;

        private static readonly List<IValueGenerator> ExternalGenerators;
        static Faker()
        {
            ExternalGenerators = new List<IValueGenerator>();
            var assembly = Assembly.LoadFile("D:\\CODE\\Csharp\\MPP_2\\FakerLibrary\\DedicatedGenerators.dll");
            foreach (var type in assembly.GetTypes())
            {
                if (type.GetInterface(nameof(IValueGenerator)) != null)
                {
                    ExternalGenerators.Add((IValueGenerator)Activator.CreateInstance(type));
                }
            }
        }
        
        public Faker()
        {
            _generators = new List<IValueGenerator>
            {
                {new IntegerGenerator()},
                {new BooleanGenerator()},
                {new DoubleGenerator()},
                {new StringGenerator()},
                {new ListGenerator()},
                {new DateTimeGenerator()}
            };

            foreach (var externalGenerator in ExternalGenerators)
            {
                _generators.Add(externalGenerator);
            }
        }

        public T Create<T>()
        {
            var generatorContext = new GeneratorContext(new Random(), this);
            generatorContext.Targets.Push(typeof(T));
            return (T) Create(typeof(T), generatorContext);
        }

        private object Create(Type targetType, GeneratorContext generatorContext)
        {
            foreach (var generator in _generators)
            {
                if (generator.CanGenerate(targetType))
                {
                    return generator.Generate(generatorContext);
                }
            }
            
            foreach (var target in generatorContext.Targets.Take(generatorContext.Targets.Count - 1))
            {
                if (target == targetType)
                {
                    var decision = generatorContext.Random.Next(0, 2);
                    if (decision == 0) 
                        return null;
                }
            } 

            var constructors = targetType.GetConstructors();
            var orderedConstructors = constructors.OrderByDescending(c => c.GetParameters().Length);

            object result = null; 
            foreach (var constructorInfo in orderedConstructors)
            {
                try
                {
                    var parametersInfo = constructorInfo.GetParameters();
                    var parameters = new List<object>();
                    foreach (var parameter in parametersInfo)
                    {
                        generatorContext.Targets.Push(parameter.ParameterType);
                        parameters.Add(Create(parameter.ParameterType, generatorContext));
                        generatorContext.Targets.Pop();
                    }

                    result = constructorInfo.Invoke(parameters.ToArray());
                    break;
                }
                catch
                {
                    //ignored
                }
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
                if (fieldInfo.IsPublic && fieldInfo.GetValue(targetObject) == CreateDefaultValue(fieldInfo.FieldType))
                {
                    generatorContext.Targets.Push(fieldInfo.FieldType);
                    fieldInfo.SetValue(targetObject, Create(fieldInfo.FieldType, generatorContext));
                    generatorContext.Targets.Pop();
                }
            }
        }
        
        private void FillPublicProperties(object targetObject, Type targetType, GeneratorContext generatorContext)
        {
            var properties = targetType.GetProperties();

            foreach (var property in properties)
            {
                if (property.CanWrite && property.GetValue(targetObject) == CreateDefaultValue(property.PropertyType))
                {
                    generatorContext.Targets.Push(property.PropertyType);
                    property.SetValue(targetObject, Create(property.PropertyType, generatorContext));
                    generatorContext.Targets.Pop();
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