using MovieSolution.Data;
using MovieSolution.Entities;
using MovieSolution.Extensions;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;
using System.Net;

namespace MovieSolution.Services
{
    public class OrderService : IOrderService
    {
        private readonly MovieShopOnlineDbContext _movieShopOnlineDbContext;
        private readonly IUserService _userService;

        private List<AddressModel> UserAddressList { get; set; } = new();


        public OrderService(MovieShopOnlineDbContext movieShopOnlineDbContext, IUserService userService)
        {
            _movieShopOnlineDbContext = movieShopOnlineDbContext;
            _userService = userService;
        }
        public async Task<Address> AddAddress(AddressModel item)
        {           
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            UserAddressList = await _userService.GetUserAddresses(item.UserId);
            if (UserAddressList.Any(address =>
               address.UserId == item.UserId &&
               address.AddressType == item.AddressType &&
               address.AddressLine == item.AddressLine &&
               address.City == item.City &&
               address.PostalCode == item.PostalCode))
            {
                // Address already exists, do nothing
                return null;
            }
            var addressToAdd = item.Convert();
            var result = await _movieShopOnlineDbContext.Addresses.AddAsync(addressToAdd);
            await _movieShopOnlineDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Order> AddOrder(OrderModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var orderToAdd = item.Convert();
            var result = await _movieShopOnlineDbContext.Orders.AddAsync(orderToAdd);
            await _movieShopOnlineDbContext.SaveChangesAsync();
            return result.Entity;           
        }

        public async Task<OrderItem> AddOrderItem(OrderItemModel item, Guid id)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
                var itemToAdd = new OrderItem
                {
                    OrderId = id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                };

                var result = await _movieShopOnlineDbContext.OrderItems.AddAsync(itemToAdd);
                await _movieShopOnlineDbContext.SaveChangesAsync();
                return result.Entity;
          
        }

        public async Task<List<OrderModel>> GetOrdersByUserId(string userId)
        {
            try
            {
                if(string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentNullException(nameof(userId));
                }

                IQueryable<Order> userOrders = _movieShopOnlineDbContext.Orders.Where(order => order.UserId == userId);
                if (userOrders == null)
                {
                    return default(List<OrderModel>);
                }
                List<OrderModel> orders = await userOrders.Convert(_movieShopOnlineDbContext);
                return orders;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
