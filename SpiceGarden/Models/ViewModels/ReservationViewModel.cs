using System.ComponentModel.DataAnnotations;

namespace SpiceGarden.Models.ViewModels;

public class ReservationViewModel
{
    [Required]
    public string CustomerName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;
    [Required]
    [FutureDate]
    public DateTime ReservationDate { get; set; }
    [Required]
    public string ReservationTime { get; set; } = string.Empty;
    [Range(1, 20)]
    public int NumberOfGuests { get; set; } = 2;
    public string? SpecialRequest { get; set; }
}

public sealed class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value) => value is DateTime date && date.Date >= DateTime.UtcNow.Date;
}
