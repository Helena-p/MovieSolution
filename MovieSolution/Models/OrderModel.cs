using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MovieSolution.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        
        public decimal OrderTotal { get; set; }
        public DateTime OrderCreatedAt { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }


    }
}
