using System;

namespace FakerLibrary.Generators
{
    public class DoubleGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            return generatorContext.Random.NextDouble();
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }
    }
}