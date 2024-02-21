using MovieSolution.Models;

namespace MovieSolution.Services.Interfaces
{
    public interface ICartService
    {
        event EventHandler CartUpdated;
        string TotalPrice { get; set; }

        string TotalQuantity { get; set; }
        Task<List<CartItemModel>> GetCartItems();
        Task SaveCartItems(List<CartItemModel> cartItems);
        Task RemoveCartItem(int productId);
        Task IncreaseQuantity(int productId);
        Task DecreaseQuantity(int productId);
    }
}
