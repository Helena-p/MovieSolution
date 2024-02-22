using MovieSolution.Models;

namespace MovieSolution.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public AddressType AddressType { get; set; }
    }
}
