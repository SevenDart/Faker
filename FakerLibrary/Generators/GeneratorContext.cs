using System;
using System.Collections.Generic;

namespace FakerLibrary.Generators
{
    public class GeneratorContext
    {
        public Random Random { get; set; }

        public IFaker Faker { get; set; }

        public Stack<Type> Targets { get; set; } = new Stack<Type>();

        public GeneratorContext(Random random, IFaker faker)
        {
            Random = random;
            Faker = faker;
        }
    }
}