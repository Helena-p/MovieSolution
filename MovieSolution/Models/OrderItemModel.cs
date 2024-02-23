using System.IO.Pipelines;

namespace MovieSolution.Models
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
    }
}
