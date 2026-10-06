using Microsoft.EntityFrameworkCore;
using SpiceGarden.Models;
using SpiceGarden.Services;

namespace SpiceGarden.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
        await context.Database.MigrateAsync();

        if (await context.Categories.AnyAsync())
        {
            return;
        }

        var starters = new Category { Name = "Starters" };
        var mains = new Category { Name = "Main Course" };
        var biryani = new Category { Name = "Biryani" };
        var desserts = new Category { Name = "Desserts" };
        var beverages = new Category { Name = "Beverages" };

        context.Categories.AddRange(starters, mains, biryani, desserts, beverages);
        await context.SaveChangesAsync();

        context.MenuItems.AddRange(
            new MenuItem { Name = "Paneer Tikka", Description = "Charred cottage cheese with peppers and yogurt marinade.", Price = 280, ImageUrl = "https://images.unsplash.com/photo-1567188040756-0c2c3e6f6f3f?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = starters },
            new MenuItem { Name = "Chicken Tikka", Description = "Tender chicken marinated in spices and roasted in a clay oven.", Price = 360, ImageUrl = "https://images.unsplash.com/photo-1601050690599-df0568f70950?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = starters },
            new MenuItem { Name = "Butter Chicken", Description = "Creamy tomato gravy with tender chicken and garlic naan.", Price = 430, ImageUrl = "https://images.unsplash.com/photo-1603894584373-5ac82b2ae398?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = mains },
            new MenuItem { Name = "Paneer Butter Masala", Description = "Soft paneer in a rich tomato, butter, and cashew sauce.", Price = 390, ImageUrl = "https://images.unsplash.com/photo-1631452180519-c014fe946bc7?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = mains },
            new MenuItem { Name = "Vegetable Biryani", Description = "Fragrant basmati rice layered with vegetables and aromatic spices.", Price = 360, ImageUrl = "https://images.unsplash.com/photo-1589302168068-964664d9517f?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = biryani },
            new MenuItem { Name = "Chicken Biryani", Description = "Aromatic basmati rice with spiced chicken and caramelized onions.", Price = 480, ImageUrl = "https://images.unsplash.com/photo-1563379926898-05f4575a45d8?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = biryani },
            new MenuItem { Name = "Garlic Naan", Description = "Soft tandoor-baked bread brushed with garlic butter.", Price = 120, ImageUrl = "https://images.unsplash.com/photo-1601050690117-94f5f6fa8bd7?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = mains },
            new MenuItem { Name = "Gulab Jamun", Description = "Warm milk dumplings soaked in fragrant rose syrup.", Price = 180, ImageUrl = "https://images.unsplash.com/photo-1551024506-0bccd828d307?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = desserts },
            new MenuItem { Name = "Mango Lassi", Description = "Cool yogurt drink with ripe mango and a touch of cardamom.", Price = 170, ImageUrl = "https://images.unsplash.com/photo-1626201320195-4e1f8a7f3f66?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = beverages },
            new MenuItem { Name = "Masala Chai", Description = "Freshly brewed tea with ginger, cardamom, and warm spices.", Price = 90, ImageUrl = "https://images.unsplash.com/photo-1571934816684-a4fe3f0acdea?auto=format&fit=crop&w=800&q=80", IsAvailable = true, Category = beverages });

        if (!await context.Users.AnyAsync())
        {
            context.Users.Add(new User
            {
                Name = "SpiceGarden Admin",
                Email = "admin@spicegarden.com",
                PasswordHash = PasswordHasher.Hash("Admin123!"),
                Role = "Admin"
            });
        }

        await context.SaveChangesAsync();
    }
}
