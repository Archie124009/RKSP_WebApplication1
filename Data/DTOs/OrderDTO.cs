namespace WebApplication1.Data.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; } 
        public int CustomerId { get; set; } 
        public List<int> ProductIds { get; set; } 
        public decimal TotalAmount { get; set; }
    }
}
