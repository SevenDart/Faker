using System;

namespace FakerLibrary.Generators
{
    public class IntegerGenerator : IValueGenerator
    {

        public object Generate(GeneratorContext generatorContext)
        {
            return generatorContext.Random.Next() * (generatorContext.Random.Next(0, 2) == 1? 1 : -1);
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(int) || type == typeof(int?);
        }
    }
}