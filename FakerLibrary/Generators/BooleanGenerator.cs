using System;

namespace FakerLibrary.Generators
{
    public class BooleanGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            var result = generatorContext.Random.Next(0, 2);
            return (result == 1);
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(bool);
        }
    }
}