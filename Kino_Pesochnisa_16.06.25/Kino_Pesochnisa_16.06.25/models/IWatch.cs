namespace Kino_Pesochnisa_16._06._25.models
{
    public class IWatch
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author {  get; set; } = string.Empty;
        public string Style {  get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Seances { get; set; } = [];
        public string? ImagePath { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Name: {Name}\nAuthor: {Author}\nStyle: {Style}\nDescription: {Description}\n Seances: {Seances}";
        }

    }
}
