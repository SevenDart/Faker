using System;

namespace FakerLibrary.Generators
{
    public interface IValueGenerator
    {
        object Generate(GeneratorContext generatorContext);

        bool CanGenerate(Type type);
    }
}