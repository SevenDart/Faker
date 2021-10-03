using System;

namespace FakerLibrary.Generators
{
    public class IntegerGenerator : IValueGenerator
    {

        public object Generate(GeneratorContext generatorContext)
        {
            return generatorContext.Random.Next();
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }
    }
}