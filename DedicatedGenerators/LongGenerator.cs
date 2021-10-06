using System;
using FakerLibrary.Generators;

namespace DedicatedGenerators
{
    public class LongGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            var upperBits = generatorContext.Random.Next();
            var lowerBits = generatorContext.Random.Next();
            var result = ((long) upperBits << 32) | (uint) lowerBits;
            return result;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(long) || type == typeof(long?);
        }
    }
}