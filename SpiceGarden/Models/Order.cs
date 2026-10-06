using System.ComponentModel.DataAnnotations;

namespace SpiceGarden.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime OrderDate { get; set; }
    [Range(0.01, 99999.99)]
    public decimal TotalAmount { get; set; }
    [Required]
    public string Status { get; set; } = "Pending";
    [Required]
    public string DeliveryAddress { get; set; } = string.Empty;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
