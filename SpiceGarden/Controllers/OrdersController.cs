using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiceGarden.Data;
using System.Security.Claims;

namespace SpiceGarden.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var orders = await _context.Orders
            .AsNoTracking().Include(o => o.OrderItems).ThenInclude(i => i.MenuItem)
            .Where(o => o.UserId == userId).OrderByDescending(o => o.OrderDate).ToListAsync();
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = await _context.Orders
            .AsNoTracking().Include(o => o.OrderItems).ThenInclude(i => i.MenuItem)
            .SingleOrDefaultAsync(o => o.Id == id && o.UserId == userId);
        if (order is null) return NotFound();
        return View(order);
    }
}
