namespace FakerLibrary
{
    public interface IFaker
    {
        T Create<T>();
    }
}