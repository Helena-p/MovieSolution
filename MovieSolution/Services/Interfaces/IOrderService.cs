using MovieSolution.Entities;
using MovieSolution.Models;

namespace MovieSolution.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> AddOrder(OrderModel item);
        Task<OrderItem> AddOrderItem(OrderItemModel item, Guid id);
        Task<Address> AddAddress(AddressModel item);
        Task<List<OrderModel>> GetOrdersByUserId(string userId);
    }
}
