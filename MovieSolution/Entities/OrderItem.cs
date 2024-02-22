namespace MovieSolution.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
