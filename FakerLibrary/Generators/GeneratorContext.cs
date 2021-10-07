using System;
using System.Collections.Generic;

namespace FakerLibrary.Generators
{
    public class GeneratorContext
    {
        public Random Random { get; }

        public IFaker Faker { get; }
        
        public FakerConfig FakerConfig { get; }

        public Stack<Type> Targets { get; } = new Stack<Type>();

        public GeneratorContext(Random random, IFaker faker)
        {
            Random = random;
            Faker = faker;
        }

        public GeneratorContext(Random random, IFaker faker, FakerConfig fakerConfig)
        {
            Random = random;
            Faker = faker;
            FakerConfig = fakerConfig;
        }
    }
}