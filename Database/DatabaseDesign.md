# SpiceGarden Database Design

## Entity Descriptions

### User

Stores a registered customer or administrator. Each user has a unique email address, a password hash, a role, and optional relationships to orders and reservations.

### Category

Groups menu items into restaurant sections such as Starters, Main Course, Biryani, Desserts, and Beverages.

### MenuItem

Represents a food item with name, description, price, image URL, availability, and category. It can appear in many order items.

### Order

Represents a customer's checkout transaction with order date, total amount, current status, delivery address, and owning user.

### OrderItem

Represents one menu item in one order, including quantity, unit price, and subtotal. Each order item belongs to both an order and a menu item.

### Reservation

Represents a customer's table booking request, including contact details, date, time, guest count, special request, and status.

## Relationships

- User → Orders: one user can place many orders.
- Order → OrderItems: one order contains many order items.
- MenuItem → OrderItems: one menu item can be present in many orders.
- Category → MenuItems: one category contains many menu items.
- User → Reservations: one user can make many reservations.
- User → User: email is unique.

All foreign-key relationships use EF Core conventions with explicit configuration in `ApplicationDbContext`. Order item deletion cascades when its order is deleted, while menu-item and category deletion are restricted to protect history.

## Table Descriptions

| Table | Purpose |
|---|---|
| Users | Registers customers and administrators. |
| Categories | Organizes menu items by section. |
| MenuItems | Stores food item details and availability. |
| Orders | Stores completed checkout transactions. |
| OrderItems | Stores individual items in each order. |
| Reservations | Stores table reservation requests. |
| __EFMigrationsHistory | Tracks applied EF Core migrations. |

## Seeded Data

The startup seeder creates five categories, ten realistic Indian meal options, and a development administrator account. The database is located at `SpiceGarden.db` and is created automatically when the application starts.
