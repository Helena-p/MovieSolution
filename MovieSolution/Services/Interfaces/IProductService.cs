using MovieSolution.Entities;
using MovieSolution.Models;

namespace MovieSolution.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductModel>> GetItems();
        Task<ProductModel> GetItemById(int id);
        Task<Product> AddItem(ProductModel item);
        Task DeleteItem(int id);
        Task<List<ProductCategory>> GetProductCategories();
    }
}
