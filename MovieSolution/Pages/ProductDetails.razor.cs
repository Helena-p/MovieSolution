using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;
using System.Text.Json;

namespace MovieSolution.Pages
{
    public partial class ProductDetails
    {
        [Parameter]
        public int ProductId { get; set; }
        [Inject]
        public ProtectedSessionStorage ProtectedSessionStore { get; set; }
        [Inject]
        public IProductService productService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }
        public ProductModel Product { get; set; } = new ProductModel();
        public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
        private string _key = "Cart";

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
            await ProtectedSessionStore.SetAsync(_key, json);
        }

        private async void AddToCart(CartItemModel cartItem)
        {
            CartItems.Add(cartItem);
            await SaveToStorage();
            navigationManager.NavigateTo(_key);
        }

        private async Task FetchCartData()
        {
            var result = await ProtectedSessionStore.GetAsync<string>(_key);
            if (!string.IsNullOrEmpty(result.Value))
            {
                CartItems = JsonSerializer.Deserialize<List<CartItemModel>>(result.Value);
                StateHasChanged();
            }
        }
    }
}
