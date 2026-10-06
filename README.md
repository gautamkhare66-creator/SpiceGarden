# SpiceGarden

SpiceGarden is a beginner-friendly ASP.NET Core MVC restaurant ordering and reservation system built for a B.Sc. Computer Science mini-project. It demonstrates CRUD operations, cookie-based authentication, password hashing, session-based shopping carts, EF Core with SQLite, and an authorized administrator dashboard.

## Objectives

- Build a professional restaurant website using ASP.NET Core MVC and Razor Views.
- Demonstrate secure user registration and role-based authorization.
- Persist menu items, orders, order lines, reservations, categories, and users using EF Core.
- Provide customer-facing ordering and table reservation workflows.
- Give administrators tools to monitor restaurant activity and manage data.

## Features

- Responsive Bootstrap 5 home page with hero, featured dishes, features, and about section.
- Searchable and filterable menu with availability and category information.
- Cookie-based customer registration, login, and logout with PBKDF2 password hashing.
- Session-based cart with add, remove, quantity updates, subtotal, total, and clear operations.
- Checkout that creates an order and order items with initial status `Pending`.
- Customer order history and order details.
- Reservation submission with pending status.
- Admin dashboard with statistics, category and menu CRUD, order status updates, reservation status updates, and user viewing.
- Friendly error handling without exposing stack traces to normal users.

## Technology Stack

- .NET 10 and ASP.NET Core MVC
- C# with nullable reference types
- Entity Framework Core with SQLite
- Razor Views and Bootstrap 5
- ASP.NET Core sessions and cookie authentication
- PBKDF2 password hashing

## System Requirements

- .NET SDK 10.0.401 or later
- SQLite support included through Microsoft.EntityFrameworkCore.Sqlite
- A modern browser

## Run

1. Open the project directory: `SpiceGarden/SpiceGarden`.
2. Restore dependencies with `dotnet restore`.
3. Create and apply the database with `dotnet ef database update`.
4. Start the application with `dotnet run`.
5. Open `http://localhost:5259` or use the URL displayed by the application.

The application automatically applies migrations and seeds the database during startup. The SQLite file is created at `SpiceGarden.db` in the project directory.

## Default/Test Credentials

- Admin: `admin@spicegarden.com`
- Password: `Admin123!`

The administrator account is created through PBKDF2 password hashing and is never displayed as a hard-coded password in the application UI. Change the seeded password in `DatabaseSeeder.cs` before sharing a production deployment.

## Modules

- Home and restaurant branding
- Menu and categories
- Account registration and login
- Cart and checkout
- Orders and order details
- Reservation creation
- Admin dashboard and management

## Database Design

See [Database/DatabaseDesign.md](Database/DatabaseDesign.md) for entity descriptions, relationships, and table documentation.

## Future Scope

- Payment gateway integration
- Email and phone notifications
- Restaurant location and delivery charges
- Product reviews and ratings
- Inventory management
- Multi-language support
- Unit and integration tests
- API versioning
