using BlazorApp2.Shared;

namespace BlazorApp2.Service
{
    public class CartService
    {
        private readonly List<Product> cartItems = new();

        public IReadOnlyList<Product> GetCartItems() => cartItems;

        public void AddToCart(Product product)
        {
            cartItems.Add(product);
        }

        public void RemoveFromCart(Product product)
        {
            cartItems.Remove(product);
        }

        public void ClearCart()
        {
            cartItems.Clear();
        }
    }
}
