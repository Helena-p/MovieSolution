using MovieSolution.Models;

namespace MovieSolution.Services.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItemModel>> GetCartItems();
        Task SaveCartItems(List<CartItemModel> cartItems);
        Task RemoveCartItem(int productId);
        Task IncreaseQuantity(int productId);
        Task DecreaseQuantity(int productId);
    }
}
