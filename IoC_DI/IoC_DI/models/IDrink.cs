namespace IoC_DI.models
{
    public interface IDrink
    {
        string Name { get; }
        string Description { get; }
        string GetInfo();
    }
}
