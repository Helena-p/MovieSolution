using MovieSolution.Data;
using MovieSolution.Entities;
using MovieSolution.Extensions;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;

namespace MovieSolution.Services
{
    public class OrderService : IOrderService
    {
        private readonly MovieShopOnlineDbContext _movieShopOnlineDbContext;


        public OrderService(MovieShopOnlineDbContext movieShopOnlineDbContext)
        {
            _movieShopOnlineDbContext = movieShopOnlineDbContext;
        }
        public async Task<Address> AddAddress(AddressModel item)
        {           
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
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
    }
}
