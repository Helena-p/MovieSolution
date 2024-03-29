﻿namespace MovieSolution.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public string ImageURL { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; } = default!;
    }
}
