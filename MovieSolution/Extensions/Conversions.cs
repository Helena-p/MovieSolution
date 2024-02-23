using Microsoft.EntityFrameworkCore;
using MovieSolution.Data;
using MovieSolution.Entities;
using MovieSolution.Models;
using MovieSolution.Pages;

namespace MovieSolution.Extensions
{   
    public static class Conversions
    {
        public static async Task<ProductModel> Convert(this Product product, MovieShopOnlineDbContext context)
        {
            var category = await context.ProductCategories.FindAsync(product.CategoryId);
            string categoryName = context != null ? category.Name : string.Empty;

            return new ProductModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Director = product.Director,
                ImageURL = product.ImageURL,
                ReleaseYear = product.ReleaseYear,
                Price = product.Price,
                CategoryId = product.CategoryId,
                CategoryName = categoryName
            };
        }
        public static async Task<List<ProductModel>> Convert(this IQueryable<Product> products, MovieShopOnlineDbContext context)
        {
            return await (from p in products
                            join c in context.ProductCategories
                            on p.CategoryId equals c.Id
                            select new ProductModel
                            {
                                Id = p.Id,
                                Title = p.Title,
                                Description = p.Description,
                                Director = p.Director,
                                ImageURL = p.ImageURL,
                                ReleaseYear = p.ReleaseYear,
                                Price = p.Price,
                                CategoryId = p.CategoryId,
                                CategoryName = c.Name
                            }).ToListAsync();
        }

        public static Product Convert(this ProductModel product)
        {
            return new Product
            {
                Title = product.Title,
                Description = product.Description,
                Director = product.Director,
                ImageURL = product.ImageURL,
                ReleaseYear = product.ReleaseYear,
                Price = product.Price,
                CategoryId = product.CategoryId,
            };
        }

        public static Address Convert(this AddressModel address)
        {
            return new Address
            {
                FirstName = address.FirstName,
                LastName = address.LastName,
                AddressLine = address.AddressLine,
                City = address.City,
                PostalCode = address.PostalCode,
                AddressType = address.AddressType,
                UserId = address.UserId,
            };
        }

        public static Order Convert(this OrderModel order)
        {
            return new Order
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderCreatedAt = order.OrderCreatedAt,
                OrderTotal = order.OrderTotal,
            };
        }

        public static async Task<List<OrderModel>> Convert(this IQueryable<Order> orders, MovieShopOnlineDbContext context)
        {
            return await(from order in orders
                         join orderItem in context.OrderItems
                         on order.Id equals orderItem.OrderId
                         select new OrderModel
                         {
                             Id = order.Id,
                             UserId = order.UserId,
                             OrderCreatedAt = order.OrderCreatedAt,
                             OrderTotal = order.OrderTotal,
                             OrderItems = context.OrderItems
                                       .Where(item => item.OrderId == order.Id)
                                       .Select(item => new OrderItemModel
                                       {
                                           Id = item.Id,
                                           OrderId = order.Id,
                                           ProductId = item.ProductId,
                                           ProductName = context.Products.SingleOrDefault(n => n.Id == item.ProductId).Title,
                                           Price = item.Price,
                                           Quantity = item.Quantity,
                                       }).ToList(),
                         }).ToListAsync();
        }
    }
}
