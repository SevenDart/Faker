using System;

namespace FakerLibrary.Generators
{
    public class GeneratorContext
    {
        public Random Random { get; set; }
        
        public Type TargetType { get; set; }
        
        public IFaker Faker { get; set; }

        public GeneratorContext(Random random, Type targetType, IFaker faker)
        {
            Random = random;
            TargetType = targetType;
            Faker = faker;
        }
    }
}