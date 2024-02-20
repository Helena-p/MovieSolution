using MovieSolution.Data;
using MovieSolution.Entities;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;
using MovieSolution.Extensions;

namespace MovieSolution.Services
{
    public class ProductService : IProductService
    {
        private readonly MovieShopOnlineDbContext _movieShopOnlineDbContext;

        public ProductService(MovieShopOnlineDbContext movieShopOnlineDbContext)
        {
            _movieShopOnlineDbContext = movieShopOnlineDbContext;
        }
        public async Task<Product> AddItem(ProductModel item)
        {
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                Product productToAdd = item.Convert();
                var result = await _movieShopOnlineDbContext.Products.AddAsync(productToAdd);
                await _movieShopOnlineDbContext.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteItem(int id)
        {
            try
            {
                var productToDelete = await _movieShopOnlineDbContext.Products.FindAsync(id);
                if (productToDelete != null)
                {
                    _movieShopOnlineDbContext.Remove(productToDelete);
                    await _movieShopOnlineDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ProductModel> GetItemById(int id)
        {
            try
            {
                var product = await _movieShopOnlineDbContext.Products.FindAsync(id);
                if (product == null)
                {
                    throw new InvalidOperationException($"No product with id:{id} was found");
                }

                var model = product.Convert(_movieShopOnlineDbContext);
                return await model;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetItemById: {ex.Message}");
                throw;
            }
        }

        public async Task<List<ProductModel>> GetItems()
        {
            try
            {
                var products = await _movieShopOnlineDbContext.Products.Convert(_movieShopOnlineDbContext);
                return products;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
