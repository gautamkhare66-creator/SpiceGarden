namespace SpiceGarden.Models.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalOrders { get; set; }
    public int PendingOrders { get; set; }
    public int TotalReservations { get; set; }
    public int PendingReservations { get; set; }
}
