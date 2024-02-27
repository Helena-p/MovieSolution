using Microsoft.EntityFrameworkCore;
using MovieSolution.Data;
using MovieSolution.Entities;
using MovieSolution.Models;
using MovieSolution.Services.Interfaces;
using System;
using System.Net.WebSockets;

namespace MovieSolution.Services
{
    public class UserService : IUserService
    {
        private readonly MovieShopOnlineDbContext _movieShopOnlineDbContext;
        public List<AddressModel> UserAddresses { get; set; } = new();

        public UserService(MovieShopOnlineDbContext movieShopOnlineDbContext)
        {
            _movieShopOnlineDbContext = movieShopOnlineDbContext;
        }
        public async Task<List<AddressModel>> GetUserAddresses(string id)
        {
            try
            {
                if(id == null)
                {
                    throw new ArgumentNullException(nameof(id));
                }

                UserAddresses = await (from a in _movieShopOnlineDbContext.Addresses
                                 where a.UserId == id
                                 select new AddressModel
                                 {
                                     Id = a.Id,
                                     UserId = a.UserId,
                                     FirstName = a.FirstName,
                                     LastName = a.LastName,
                                     AddressLine = a.AddressLine,
                                     PostalCode = a.PostalCode,
                                     City = a.City,
                                     AddressType = a.AddressType,
                                 }).ToListAsync();
      
                return UserAddresses;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
