using System;

namespace FakerLibrary.Generators
{
    public class StringGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            var length = generatorContext.Random.Next(1000000);
            var result = "";
            for (var i = 0; i < length; i++)
            {
                var nextChar = (char)generatorContext.Random.Next(255);
                result += nextChar;
            }

            return result;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }
    }
}