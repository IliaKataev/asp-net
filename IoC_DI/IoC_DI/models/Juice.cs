namespace IoC_DI.models
{
    public class Juice : IDrink
    {
        public string Name => "Яблочный";

        public string Description => "Деревенские яблочки";

        public string GetInfo() => $"{Name}: {Description}";
    }

    //public class Programm
    //{
    //    IDrink drink;
    //    public void Method()
    //    {
    //        var drink = new Tea();
    //    }

    //    public Programm(IDrink drink)
    //    {
    //        this.drink = drink;
    //    }
    //}
}
