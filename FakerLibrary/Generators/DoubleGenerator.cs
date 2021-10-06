using System;

namespace FakerLibrary.Generators
{
    public class DoubleGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            var result = generatorContext.Random.NextDouble();
            var power = Math.Pow(10, generatorContext.Random.Next(0, 63));
            return result * power;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }
    }
}