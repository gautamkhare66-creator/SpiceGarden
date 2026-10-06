using Microsoft.AspNetCore.Http;
using SpiceGarden.Models;
using System.Text.Json;

namespace SpiceGarden.Services;

public static class CartService
{
    private const string CartSessionKey = "SpiceGardenCart";

    public static List<CartItem> GetCart(HttpContext context)
        => JsonSerializer.Deserialize<List<CartItem>>(context.Session.GetString(CartSessionKey) ?? "[]") ?? [];

    public static void AddItem(HttpContext context, int menuItemId, string name, decimal price, string imageUrl)
    {
        var cart = GetCart(context);
        var existing = cart.FirstOrDefault(i => i.MenuItemId == menuItemId);
        if (existing is null)
        {
            cart.Add(new CartItem { MenuItemId = menuItemId, Name = name, Price = price, ImageUrl = imageUrl, Quantity = 1 });
        }
        else
        {
            existing.Quantity++;
        }
        Save(context, cart);
    }

    public static void UpdateQuantity(HttpContext context, int menuItemId, int quantity)
    {
        var cart = GetCart(context);
        var item = cart.FirstOrDefault(i => i.MenuItemId == menuItemId);
        if (item is null) return;
        if (quantity <= 0) cart.Remove(item);
        else item.Quantity = quantity;
        Save(context, cart);
    }

    public static void RemoveItem(HttpContext context, int menuItemId)
    {
        var cart = GetCart(context);
        cart.RemoveAll(i => i.MenuItemId == menuItemId);
        Save(context, cart);
    }

    public static void Clear(HttpContext context)
        => context.Session.Remove(CartSessionKey);

    public static int GetItemCount(HttpContext context)
        => GetCart(context).Sum(i => i.Quantity);

    public static decimal GetSubtotal(HttpContext context)
        => GetCart(context).Sum(i => i.Subtotal);

    private static void Save(HttpContext context, List<CartItem> cart)
        => context.Session.SetString(CartSessionKey, JsonSerializer.Serialize(cart));
}
