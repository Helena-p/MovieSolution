using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MovieSolution.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using MovieSolution.Services.Interfaces;

namespace MovieSolution.Pages
{
    public partial class Checkout
    {
        [Inject]
        public ICartService CartService { get; set; }
        [Inject]
        public ProtectedSessionStorage ProtectedSessionStore { get; set; }
        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public AddressModel BillingAddress { get; set; } = new();
        public AddressModel ShippingAddress { get; set; } = new();
        public OrderModel Order { get; set; } = new();
        public bool UseBillingAsShipping { get; set; } = true;
        public List<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
        public List<OrderItemModel> OrderItems { get; set; } = new();
        private string UserId { get; set; } = string.Empty;

        protected override Task OnInitializedAsync()
        {
            UserId = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                CartItems = await CartService.GetCartItems();
            }
        }

        private async Task SaveBillingAddress()
        {
            BillingAddress = new AddressModel
            {
                FirstName = BillingAddress.FirstName,
                LastName = BillingAddress.LastName,
                AddressLine = BillingAddress.AddressLine,
                City = BillingAddress.City,
                PostalCode = BillingAddress.PostalCode,
                AddressType = AddressType.Billing,
                UserId = UserId,
            };
        }

        private async Task SaveShippingAddress()
        {
            ShippingAddress = new AddressModel
            {
                FirstName = ShippingAddress.FirstName,
                LastName = ShippingAddress.LastName,
                AddressLine = ShippingAddress.AddressLine,
                City = ShippingAddress.City,
                PostalCode = ShippingAddress.PostalCode,
                AddressType = AddressType.Shipping,
                UserId = UserId,
            };
        }

        private async Task SubmitForm()
        {
            if (!UseBillingAsShipping)
            {
                // Submit both billing and shipping addresses
                await SaveBillingAddress();
                await SaveShippingAddress();
                
            }
            else
            {
                // Submit only billing address
                await SaveBillingAddress();
               
            }

        }

        private void HandleInvalidSubmit()
        {
            // Handle invalid submission for both forms
        }

        public async Task CreateOrder()
        {
            Order = new OrderModel
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                OrderCreatedAt = DateTime.Now,
                OrderTotal = CartItems.Sum(i => i.Quantity * i.Price),
                OrderItems = CartItems.Select(i => new OrderItemModel
                {
                    OrderId = Order.Id,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
            await CartService.ClearSessionStorage();
        }
    }
}
