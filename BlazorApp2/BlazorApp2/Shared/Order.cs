namespace BlazorApp2.Shared
{
    public class Order
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public List<Product> Products { get; set; } = new();
    }
}
