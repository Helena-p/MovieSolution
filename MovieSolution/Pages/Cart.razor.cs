using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors.Infrastructure;
using MovieSolution.Models;
using MovieSolution.Services;
using MovieSolution.Services.Interfaces;

namespace MovieSolution.Pages
{
    public partial class Cart
    {
        [Inject]
        public ICartService CartService { get; set; }

        public List<CartItemModel> CartItems { get; set; }
        private string TotalPrice { get; set; } = string.Empty;
        private string TotalQuantity { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            CartItems = new List<CartItemModel>();
            // Listens for Cart event
            CartService.CartUpdated += OnCartUpdated;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CartItems = await CartService.GetCartItems();
                StateHasChanged();
            }
        }

        private void OnCartUpdated(object sender, EventArgs e)
        {
            TotalPrice = CartService.TotalPrice;
            TotalQuantity = CartService.TotalQuantity;
            StateHasChanged();
        }

        public void Dispose()
        {
            CartService.CartUpdated -= OnCartUpdated;
        }

        private async Task RemoveItem(int productId)
        {
            await CartService.RemoveCartItem(productId);
            CartItems = await CartService.GetCartItems();
            StateHasChanged();
        }

        private async Task DecreaseQty(CartItemModel item)
        {
            await CartService.DecreaseQuantity(item.ProductId);
            CartItems = await CartService.GetCartItems();
            StateHasChanged();
        }

        private async Task IncreaseQty(CartItemModel item)
        {
            await CartService.IncreaseQuantity(item.ProductId);
            CartItems = await CartService.GetCartItems();
            StateHasChanged();
        }
    }
}
