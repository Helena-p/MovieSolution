using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;
using System.Text.Json;

namespace MovieSolution.Services
{
    public class CartService : ICartService
    {
        protected ProtectedSessionStorage ProtectedSessionStore { get; set; }
        public List<CartItemModel> cartItems { get; set; } = new List<CartItemModel>();
        public string TotalPrice { get; set; } = string.Empty;
        public string TotalQuantity { get; set; } = string.Empty;
        private string _key = "Cart";
        public event EventHandler CartUpdated;

        public CartService(ProtectedSessionStorage protectedSessionStorage)
        {
            ProtectedSessionStore = protectedSessionStorage;
        }
        public async Task DecreaseQuantity(int productId)
        {
            var items = await GetCartItems();
            var item = items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null && item.Quantity > 1)
            {
                item.Quantity--;
                await SaveCartItems(items);
            }
        }

        public async Task<List<CartItemModel>> GetCartItems()
        {
            try
            {
                var result = await ProtectedSessionStore.GetAsync<string>(_key);
                if (!result.Success)
                {
                    return default(List<CartItemModel>);
                }

                cartItems = JsonSerializer.Deserialize<List<CartItemModel>>(result.Value);
                CalculateCartSummaryTotals(cartItems);
                return cartItems;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting cart items: {ex.Message}");
                throw;
            }
        }

        public async Task IncreaseQuantity(int productId)
        {
            var items = await GetCartItems();
            var item = items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                item.Quantity++;
                await SaveCartItems(items);
            }
        }

        public async Task RemoveCartItem(int productId)
        {
            var items = await GetCartItems();
            if (items.Count != 0)
            {
                items.RemoveAll(item => item.ProductId == productId);
                await SaveCartItems(items);
            }

        }

        public async Task SaveCartItems(List<CartItemModel> items)
        {
            try
            {
                var json = JsonSerializer.Serialize(items);
                await ProtectedSessionStore.SetAsync(_key, json);
                CalculateCartSummaryTotals(items);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving cart items: {ex.Message}");
                throw;
            }
        }

        public void CalculateCartSummaryTotals(List<CartItemModel> items)
        {
            TotalPrice = items.Sum(p => p.Quantity * p.Price).ToString("C");
            TotalQuantity = items.Sum(p => p.Quantity).ToString();
            // Notify subscribers whenever the cart changes
            CartUpdated?.Invoke(this, EventArgs.Empty);
        }

    }
}
