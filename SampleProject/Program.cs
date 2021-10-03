using System;
using FakerLibrary;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var faker = new Faker();
            for (int i = 0; i < 1000; i++)
            {
                var testDouble = faker.Create<double>();
                Console.WriteLine(testDouble);
            }
        }
    }
}