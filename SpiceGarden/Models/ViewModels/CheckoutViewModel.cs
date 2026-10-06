using System.ComponentModel.DataAnnotations;

namespace SpiceGarden.Models.ViewModels;

public class CheckoutViewModel
{
    [Required]
    [Display(Name = "Customer name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Delivery address")]
    public string DeliveryAddress { get; set; } = string.Empty;

    [Phone]
    public string? Phone { get; set; }
}
