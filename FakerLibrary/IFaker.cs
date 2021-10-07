namespace FakerLibrary
{
    public interface IFaker
    {
        T Create<T>();
        T Create<T>(FakerConfig fakerConfig);
    }
}