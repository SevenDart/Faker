using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FakerLibrary.Generators
{
    public class ListGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            var targetType = generatorContext.Targets.Peek();
            var genericTypeArgument = targetType.GenericTypeArguments[0];
            var enumerable = Activator.CreateInstance(targetType);

            var length = generatorContext.Random.Next(1000000);

            var fakerMethods = typeof(IFaker).GetMethods();
            MethodInfo executingMethod = fakerMethods[0];
            foreach (var fakerMethod in fakerMethods)
            {
                if (fakerMethod.GetParameters().Length == 1)
                    executingMethod = fakerMethod;
            }

            executingMethod = executingMethod.MakeGenericMethod(genericTypeArgument);
            
            for (int i = 0; i < length; i++)
            {
                var obj = new []{
                    executingMethod.Invoke(generatorContext.Faker, new object[]{generatorContext.FakerConfig})
                };

                targetType.GetMethod("Add")?.Invoke(enumerable, obj);
            }

            return enumerable;
        }

        public bool CanGenerate(Type type)
        {
            var interfaces = type.GetInterfaces();
            return interfaces.Contains(typeof(IList)) && type.IsGenericType;
        }
    }
}