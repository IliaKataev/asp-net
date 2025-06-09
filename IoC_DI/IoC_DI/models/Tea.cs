namespace IoC_DI.models
{
    public class Tea : IDrink
    {
        public string Name => "Чай";
        public string Description => "Голубой улун";
        public string GetInfo() => $"{Name}: {Description}";
    }
}
