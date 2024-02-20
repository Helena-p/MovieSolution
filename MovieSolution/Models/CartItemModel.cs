namespace MovieSolution.Models
{
    public class CartItemModel
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
