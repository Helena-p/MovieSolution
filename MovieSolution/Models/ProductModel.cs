using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MovieSolution.Models
{
    public class ProductModel
    {
        [BindNever]
        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Director name cannot exceed 100 characters")]
        public string Director { get; set; } = string.Empty;
        [Required]
        public string ImageURL { get; set; } = string.Empty;
        [Required]
        public int ReleaseYear { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [BindNever]
        public string CategoryName { get; set; } = string.Empty;
    }
}
