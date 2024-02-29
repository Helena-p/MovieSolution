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
        public IOrderService OrderService { get; set; }
        [Inject]
        public ProtectedSessionStorage ProtectedSessionStore { get; set; }
        [Inject]
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public AddressModel BillingAddress { get; set; } = new();
        public AddressModel ShippingAddress { get; set; } = new();
        public OrderModel Order { get; set; } = new();
        public bool UseBillingAsShipping { get; set; } = true;
        public List<CartItemModel> CartItems { get; set; } = new();
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

        private async Task SaveAddress(AddressModel address, AddressType addressType)
        {          
            address.UserId = UserId;
            address.AddressType = addressType;
            var result = await OrderService.AddAddress(address);
            if(result == null)
            {
                Console.WriteLine("Address already exist");
            }
            else
            {
                Console.WriteLine("Address added successfully");
            }
        }

        private async Task SubmitForm()
        {
            if (!UseBillingAsShipping)
            {
                // Submit both billing and shipping addresses
                await SaveAddress(BillingAddress, AddressType.Billing);
                await SaveAddress(ShippingAddress, AddressType.Shipping);

            }
            else
            {
                // Submit only billing address
                await SaveAddress(BillingAddress, AddressType.Billing);

            }
        }

        public async Task PlaceOrder()
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
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            await SubmitForm();

            await CreateOrder();
        }
        public async Task CreateOrder()
        {
            await OrderService.AddOrder(Order);
            await SaveOrderItems(Order.OrderItems, Order.Id);

            await CartService.ClearSessionStorage();
            NavigationManager.NavigateTo("orderconfirm", forceLoad: true);
        }

        private async Task SaveOrderItems(List<OrderItemModel> orderItems, Guid id)
        {
            foreach (var orderItem in orderItems)
            {
                await OrderService.AddOrderItem(orderItem, id);
            }
        }    
    }
}
