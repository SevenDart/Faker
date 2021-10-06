using System;
using FakerLibrary.Generators;

namespace DedicatedGenerators
{
    public class FloatGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            var result = (float) generatorContext.Random.NextDouble();
            var power = (float) Math.Pow(10, generatorContext.Random.Next(0, 31));
            return result * power;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(float) || type == typeof(float?);
        }
    }
}