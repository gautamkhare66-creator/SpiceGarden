using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiceGarden.Data;
using SpiceGarden.Models;
using SpiceGarden.Models.ViewModels;

namespace SpiceGarden.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            TotalUsers = await _context.Users.CountAsync(),
            TotalOrders = await _context.Orders.CountAsync(),
            PendingOrders = await _context.Orders.CountAsync(o => o.Status == "Pending"),
            TotalReservations = await _context.Reservations.CountAsync(),
            PendingReservations = await _context.Reservations.CountAsync(r => r.Status == "Pending")
        };
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Categories() => View(await _context.Categories.OrderBy(c => c.Name).ToListAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddCategory(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) return RedirectToAction(nameof(Categories));
        _context.Categories.Add(new Category { Name = name.Trim() });
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Categories));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCategory(int id, string name)
    {
        var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        if (category is not null && !string.IsNullOrWhiteSpace(name))
        {
            category.Name = name.Trim();
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Categories));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.SingleOrDefaultAsync(c => c.Id == id);
        if (category is not null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Categories));
    }

    [HttpGet]
    public async Task<IActionResult> Menu() => View(await _context.MenuItems.Include(m => m.Category).OrderBy(m => m.Name).ToListAsync());

    [HttpGet]
    public async Task<IActionResult> AddMenuItem()
    {
        ViewData["Categories"] = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        return View(new MenuItem());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMenuItem(MenuItem item)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
            return View(item);
        }
        _context.MenuItems.Add(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Menu));
    }

    [HttpGet]
    public async Task<IActionResult> EditMenuItem(int id)
    {
        var item = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
        if (item is null) return NotFound();
        ViewData["Categories"] = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        return View("EditMenuItem", item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditMenuItem(MenuItem item)
    {
        if (!ModelState.IsValid) return View("EditMenuItem", item);
        var existing = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == item.Id);
        if (existing is null) return NotFound();
        existing.Name = item.Name;
        existing.Description = item.Description;
        existing.Price = item.Price;
        existing.ImageUrl = item.ImageUrl;
        existing.IsAvailable = item.IsAvailable;
        existing.CategoryId = item.CategoryId;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Menu));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteMenuItem(int id)
    {
        var item = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
        if (item is not null) _context.MenuItems.Remove(item);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Menu));
    }

    [HttpGet]
    public async Task<IActionResult> Orders() => View(await _context.Orders.Include(o => o.User).OrderByDescending(o => o.OrderDate).ToListAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateOrderStatus(int id, string status)
    {
        var order = await _context.Orders.SingleOrDefaultAsync(o => o.Id == id);
        if (order is null) return NotFound();
        order.Status = status;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Orders));
    }

    [HttpGet]
    public async Task<IActionResult> Reservations() => View(await _context.Reservations.OrderByDescending(r => r.ReservationDate).ToListAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateReservationStatus(int id, string status)
    {
        var reservation = await _context.Reservations.SingleOrDefaultAsync(r => r.Id == id);
        if (reservation is null) return NotFound();
        reservation.Status = status;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Reservations));
    }

    [HttpGet]
    public async Task<IActionResult> Users() => View(await _context.Users.OrderBy(u => u.Name).ToListAsync());
}
