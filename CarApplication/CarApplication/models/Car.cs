namespace CarApplication.models
{
    public class Car
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Manufacturer { get; set; }
        public int Year { get; set; }
        public double EngineVolume { get; set; }

        public override string ToString()
        {
            return $"Название: {Name}, Цвет: {Color}, Производитель: {Manufacturer}, Год выпуска {Year}, Объём двигателя {EngineVolume}";
        }
    }
}
