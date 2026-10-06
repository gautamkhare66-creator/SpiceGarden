using System.ComponentModel.DataAnnotations;

namespace SpiceGarden.Models.ViewModels;

public class MenuSearchViewModel
{
    public int? CategoryId { get; set; }
    [StringLength(50)]
    public string? Search { get; set; }
}
