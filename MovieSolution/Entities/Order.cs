namespace MovieSolution.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderCreatedAt { get; set; }
    }
}
