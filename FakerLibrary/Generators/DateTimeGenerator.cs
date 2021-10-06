using System;

namespace FakerLibrary.Generators
{
    public class DateTimeGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext generatorContext)
        {
            long ticks = 0;
            do
            {
                var upperBits = generatorContext.Random.Next();
                var lowerBits = generatorContext.Random.Next();
                ticks = ((long) upperBits << 32) | (uint) lowerBits;
            } while (ticks > DateTime.MaxValue.Ticks || ticks < DateTime.MinValue.Ticks);
            var result = new DateTime(ticks);
            return result;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(DateTime) || type == typeof(DateTime?);
        }
    }
}