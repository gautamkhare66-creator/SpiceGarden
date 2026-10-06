using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiceGarden.Data;
using SpiceGarden.Models;
using SpiceGarden.Models.ViewModels;
using SpiceGarden.Services;

namespace SpiceGarden.Controllers;

public class MenuController : Controller
{
    private readonly ApplicationDbContext _context;

    public MenuController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> Index(MenuSearchViewModel? model)
    {
        model ??= new MenuSearchViewModel();
        var query = _context.MenuItems.AsQueryable().Where(m => m.IsAvailable);
        if (model.CategoryId.HasValue)
            query = query.Where(m => m.CategoryId == model.CategoryId.Value);
        if (!string.IsNullOrWhiteSpace(model.Search))
            query = query.Where(m => m.Name.Contains(model.Search.Trim()) || m.Description.Contains(model.Search.Trim()));

        var categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        var items = await query.Include(m => m.Category).OrderBy(m => m.Name).ToListAsync();
        ViewData["Categories"] = categories;
        ViewData["MenuItems"] = items;
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int menuItemId)
    {
        var item = await _context.MenuItems.AsNoTracking().SingleOrDefaultAsync(m => m.Id == menuItemId && m.IsAvailable);
        if (item is null) return NotFound();
        CartService.AddItem(HttpContext, item.Id, item.Name, item.Price, item.ImageUrl);
        TempData["Success"] = $"{item.Name} was added to your cart.";
        return RedirectToAction("Index", "Menu");
    }
}
