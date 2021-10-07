using System;
using System.Text;

namespace FakerLibrary.Generators
{
    public class StringGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            var length = generatorContext.Random.Next(1000000);
            StringBuilder result = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                var nextChar = (char)generatorContext.Random.Next(255);
                result.Append(nextChar);
            }

            return result.ToString();
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }
    }
}