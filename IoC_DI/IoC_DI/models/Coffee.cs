namespace IoC_DI.models
{
    public class Coffee : IDrink
    {
        public string Name => "Кофе";

        public string Description => "Бамбл на апельсиновом соке";

        public string GetInfo() => $"{Name}: {Description}";
    }
}
