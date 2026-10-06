using System.ComponentModel.DataAnnotations;

namespace SpiceGarden.Models;

public class Reservation
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public User? User { get; set; }
    [Required]
    public string CustomerName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public DateTime ReservationDate { get; set; }
    [Required]
    public string ReservationTime { get; set; } = string.Empty;
    [Range(1, 20)]
    public int NumberOfGuests { get; set; }
    public string SpecialRequest { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
}
