using MovieSolution.Models;

namespace MovieSolution.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<AddressModel>> GetUserAddresses(string id);
    }
}
