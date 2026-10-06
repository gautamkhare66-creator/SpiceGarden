using Microsoft.AspNetCore.Mvc;
using SpiceGarden.Models;
using SpiceGarden.Services;

namespace SpiceGarden.Controllers;

public class CartController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ViewData["Cart"] = CartService.GetCart(HttpContext);
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateQuantity(int menuItemId, int quantity)
    {
        CartService.UpdateQuantity(HttpContext, menuItemId, quantity);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int menuItemId)
    {
        CartService.RemoveItem(HttpContext, menuItemId);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        CartService.Clear(HttpContext);
        return RedirectToAction(nameof(Index));
    }
}
