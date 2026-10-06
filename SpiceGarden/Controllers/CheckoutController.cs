using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpiceGarden.Data;
using SpiceGarden.Models;
using SpiceGarden.Models.ViewModels;
using SpiceGarden.Services;
using System.Security.Claims;

namespace SpiceGarden.Controllers;

[Authorize]
public class CheckoutController : Controller
{
    private readonly ApplicationDbContext _context;

    public CheckoutController(ApplicationDbContext context) => _context = context;

    [HttpGet]
    public IActionResult Index()
    {
        var cart = CartService.GetCart(HttpContext);
        if (cart.Count == 0) return RedirectToAction("Index", "Cart");
        ViewData["Cart"] = cart;
        return View(new CheckoutViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
    {
        if (!ModelState.IsValid) return View("Index", model);
        var cart = CartService.GetCart(HttpContext);
        if (cart.Count == 0) return RedirectToAction("Index", "Cart");

        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = CartService.GetSubtotal(HttpContext),
            Status = "Pending",
            DeliveryAddress = model.DeliveryAddress.Trim(),
            OrderItems = cart.Select(item => new OrderItem
            {
                MenuItemId = item.MenuItemId,
                Quantity = item.Quantity,
                UnitPrice = item.Price,
                Subtotal = item.Subtotal
            }).ToList()
        };

        await using var transaction = await _context.Database.BeginTransactionAsync();
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        foreach (var item in order.OrderItems)
            item.OrderId = order.Id;
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        CartService.Clear(HttpContext);
        TempData["Success"] = $"Order #{order.Id} has been placed successfully.";
        return RedirectToAction("Details", "Orders", new { id = order.Id });
    }
}
