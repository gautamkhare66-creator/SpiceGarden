using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiceGarden.Data;
using SpiceGarden.Models;
using SpiceGarden.Models.ViewModels;
using System.Security.Claims;

namespace SpiceGarden.Controllers;

public class ReservationController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReservationController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult Index() => View(new ReservationViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservationViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) is string id ? int.Parse(id) : (int?)null;
        var reservation = new Reservation
        {
            UserId = userId,
            CustomerName = model.CustomerName.Trim(),
            Email = model.Email.Trim(),
            Phone = model.Phone.Trim(),
            ReservationDate = model.ReservationDate.Date,
            ReservationTime = model.ReservationTime,
            NumberOfGuests = model.NumberOfGuests,
            SpecialRequest = model.SpecialRequest?.Trim() ?? string.Empty,
            Status = "Pending"
        };
        _context.Reservations.Add(reservation);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Your reservation request has been submitted.";
        return RedirectToAction("Confirmation");
    }

    [Authorize]
    public async Task<IActionResult> Confirmation()
    {
        var reservations = await _context.Reservations.AsNoTracking().OrderByDescending(r => r.Id).Take(5).ToListAsync();
        ViewData["Reservations"] = reservations;
        return View();
    }
}
