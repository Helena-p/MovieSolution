using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MovieSolution.Models
{
    public class ProductModel
    {
        [BindNever]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "{0} cannot exceed {1} characters, or be less than {2}", MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [StringLength(200, ErrorMessage = "{0} cannot exceed {1} characters, or be less than {2}", MinimumLength = 2)]
        public string Description { get; set; } = string.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "{0} cannot exceed {1} characters, or be less than {2}", MinimumLength = 2)]
        public string Director { get; set; } = string.Empty;
        [Required]
        [Url]
        public string ImageURL { get; set; } = string.Empty;
        [Required]
        [Range(1900, 9999, ErrorMessage = "{0} must range between {1} and {2}")]
        public int ReleaseYear { get; set; }
        [Required]
        [Range(1, 100, ErrorMessage = "{0} must range between {1} and {2}")]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [BindNever]
        public string CategoryName { get; set; } = string.Empty;
    }
}
