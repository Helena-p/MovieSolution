using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;
using System.Text.Json;

namespace MovieSolution.Pages
{
    public partial class ProductDetails
    {
        [Inject]
        public ProtectedSessionStorage ProtectedSessionStore { get; set; }

        public ProductDetails()
        {
        }

        [Parameter]
        public int ProductId { get; set; }
        [Inject]
        public IProductService productService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        public ProductModel Product { get; set; } = new ProductModel();
        public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await FetchCartData();
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            Product = await productService.GetItemById(ProductId);
        }

        private async Task SaveToStorage()
        {
            var json = JsonSerializer.Serialize(CartItems);
            await ProtectedSessionStore.SetAsync("Cart", json);
        }

        private async void AddToCart(CartItemModel cartItem)
        {
            CartItems.Add(cartItem);
            await SaveToStorage();
            navigationManager.NavigateTo("cart");
        }

        private async Task FetchCartData()
        {
            var result = await ProtectedSessionStore.GetAsync<string>("Cart");
            if (!string.IsNullOrEmpty(result.Value))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItemModel>>(result.Value);
                StateHasChanged();
            }
        }
    }
}
