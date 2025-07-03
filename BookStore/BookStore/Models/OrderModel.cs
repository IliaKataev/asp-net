using System.Text.Json.Serialization;

namespace BookStore.Models
{
    public class OrderModel
    {
        [JsonPropertyName("items")]
        public List<string> Items { get; set; } = new();
    }
}
