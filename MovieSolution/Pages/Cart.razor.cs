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
        public ICartService cartService { get; set; }

        public Cart()
        {
        }

        public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
        private string totalPrice { get; set; } = string.Empty;
        private string totalQuantity { get; set; } = string.Empty;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            // Listens for Cart event
            cartService.CartUpdated += OnCartUpdated;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CartItems = await cartService.GetCartItems();
                StateHasChanged();
            }
        }

        private void OnCartUpdated(object sender, EventArgs e)
        {
            totalPrice = cartService.TotalPrice;
            totalQuantity = cartService.TotalQuantity;
            StateHasChanged();
        }

        public void Dispose()
        {
            cartService.CartUpdated -= OnCartUpdated;
        }

        private async Task RemoveItem(int productId)
        {
            await cartService.RemoveCartItem(productId);
            CartItems = await cartService.GetCartItems();
            StateHasChanged();
        }

        private async Task DecreaseQty(CartItemModel item)
        {
            await cartService.DecreaseQuantity(item.ProductId);
            CartItems = await cartService.GetCartItems();
            StateHasChanged();
        }

        private async Task IncreaseQty(CartItemModel item)
        {
            await cartService.IncreaseQuantity(item.ProductId);
            CartItems = await cartService.GetCartItems();
            StateHasChanged();
        }
    }
}
