using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MovieSolution.Models
{
    public class AddressModel
    {
        [BindNever]
        public int Id { get; set; }
        [BindNever]
        public string UserId { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed more than 200 characters")]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed more than 200 characters")]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MaxLength(300, ErrorMessage = "Address cannot exceed more than 300 characters")]
        public string AddressLine { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Postal code cannot consist of less than 5 characters")]
        public string PostalCode { get; set; } = string.Empty;
        [Required]
        [MaxLength (200, ErrorMessage = "City cannot exceed more than 200 characters")]
        public string City { get; set; } = string.Empty;
        [BindNever]
        public AddressType AddressType { get; set; }
    }
}
