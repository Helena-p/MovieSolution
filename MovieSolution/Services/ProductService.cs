using MovieSolution.Data;
using MovieSolution.Entities;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;
using MovieSolution.Extensions;
using Microsoft.EntityFrameworkCore;

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
                var product = await _movieShopOnlineDbContext.Products.FindAsync(id) 
                    ?? throw new InvalidOperationException($"No product with id:{id} was found");
                var productModel = product.Convert(_movieShopOnlineDbContext);
                return await productModel;
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

        public async Task<List<ProductCategory>>? GetProductCategories()
        {
           List<ProductCategory> categories = await _movieShopOnlineDbContext.ProductCategories.ToListAsync();
            if(categories != null)
            {
                return categories;
            }
            return null;
        }

        public async Task<List<OrderItem>> GetMostPopularOrderItems()
        {
            /*
             * Sum qty for individual products
             * Create a group-list of the four items with highest totalqty
             */
            var itemsByQuantity = await _movieShopOnlineDbContext.OrderItems
                .GroupBy(item => item.ProductId)
                .Select(group => new { ProductId = group.Key, TotalQuantity = group.Sum(entity => entity.Quantity) })
                .OrderByDescending(e1 => e1.TotalQuantity)
                .Take(4)
                .ToListAsync();

            // Retrieve the OrderItem objects based on the group-list from DB
                var orderItems = new List<OrderItem>();
                foreach (var item in itemsByQuantity)
                {
                    var orderItem = await _movieShopOnlineDbContext.OrderItems
                        .FirstOrDefaultAsync(o => o.ProductId == item.ProductId);
                    if (orderItem != null)
                    {
                        orderItems.Add(orderItem);
                    }
                }

            return orderItems;
        }
    }
}
