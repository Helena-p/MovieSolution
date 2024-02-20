using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Cors.Infrastructure;
using MovieSolution.Models;
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
        protected string TotalPrice { get; set; } = string.Empty;
        protected string TotalQuantity { get; set; } = string.Empty;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CartItems = await cartService.GetCartItems();
                CalculateCartSummaryTotals();
                StateHasChanged();
            }
        }

        private async Task RemoveItem(int productId)
        {
            await cartService.RemoveCartItem(productId);
            CartItems = await cartService.GetCartItems();
            CalculateCartSummaryTotals();
            StateHasChanged();
        }

        private async Task DecreaseQty(CartItemModel item)
        {
            await cartService.DecreaseQuantity(item.ProductId);
            CartItems = await cartService.GetCartItems();
            CalculateCartSummaryTotals();
            StateHasChanged();
        }

        private async Task IncreaseQty(CartItemModel item)
        {
            await cartService.IncreaseQuantity(item.ProductId);
            CartItems = await cartService.GetCartItems();
            CalculateCartSummaryTotals();
            StateHasChanged();
        }

        private void CalculateCartSummaryTotals()
        {
            TotalPrice = CartItems.Sum(p => p.Quantity * p.Price).ToString("C");
            TotalQuantity = CartItems.Sum(p => p.Quantity).ToString();
        }
    }
}
