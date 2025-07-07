using BlazorApp2.Shared;
using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace BlazorApp2.Service
{
    public class CartService
    {
        private readonly List<Product> cartItems = new();
        private readonly ILocalStorageService localStorage;
        private readonly string KeyStorage = "cartItems";

        public CartService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public IReadOnlyList<Product> GetCartItems() => cartItems;

        public async Task AddToCart(Product product)
        {
            cartItems.Add(product);
            await SaveCart();
        }

        public async Task RemoveFromCart(Product product)
        {
            cartItems.Remove(product);
            await SaveCart();
        }

        public async Task ClearCart()
        {
            cartItems.Clear();
            await SaveCart();
        }

        public async Task LoadCart()
        {
            var savedCart = await localStorage.GetItemAsync<List<Product>>(KeyStorage);
            if(savedCart != null)
            {
                cartItems.Clear();
                cartItems.AddRange(savedCart);
            }
        }

        private async Task SaveCart()
        {
            await localStorage.SetItemAsync(KeyStorage, cartItems);
        }
    }
}
