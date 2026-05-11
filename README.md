# CleanCommerce

CleanCommerce is a learning-focused e-commerce Web API built with ASP.NET Core, Entity Framework Core, SQL Server, JWT authentication, and layered architecture.

The goal of this project is not to build a complete production e-commerce platform. The goal is to demonstrate a clean backend structure where API controllers, business logic, domain models, and infrastructure concerns are separated into different projects.

## Project Status

Current implemented scope:

- Category management
- Product management
- User registration and login
- JWT authentication
- Role-based authorization
- Cart management for authenticated users
- Order checkout from cart
- Stock validation during checkout
- Transactional checkout using a Unit of Work abstraction
- Global exception handling middleware
- Request/response DTO pattern
- AutoMapper mapping profiles
- SQL Server persistence with EF Core code-first migrations

## Tech Stack

- C#
- ASP.NET Core Web API
- .NET 10
- Entity Framework Core
- SQL Server
- JWT Bearer Authentication
- Role-based Authorization
- AutoMapper
- BCrypt.Net-Next
- Swagger / OpenAPI
- Layered Architecture

## Solution Structure

```text
CleanCommerce
├── CleanCommerce.Api
├── CleanCommerce.Application
├── CleanCommerce.Domain
├── CleanCommerce.Infrastructure
└── CleanCommerce.slnx
```

## Layer Responsibilities

### CleanCommerce.Api

The API layer is responsible for HTTP-related behavior.

It contains:

- Controllers
- Program.cs
- Swagger configuration
- JWT middleware setup
- Global exception middleware
- API pipeline configuration

It should not contain business logic or direct EF Core database logic.

### CleanCommerce.Application

The Application layer contains the use-case logic of the system.

It contains:

- DTOs
- Service interfaces
- Service implementations
- Repository interfaces
- Security interfaces
- AutoMapper profiles
- Custom exceptions
- Application dependency injection setup

This layer coordinates business rules such as product validation, cart ownership checks, checkout rules, and order creation.

### CleanCommerce.Domain

The Domain layer contains the core business models.

It contains:

- Entities
- Enums

Main entities include:

- User
- Role
- UserRole
- Category
- Product
- CartItem
- Order
- OrderItem

The Domain layer should stay independent from ASP.NET Core, EF Core, JWT, and infrastructure details.

### CleanCommerce.Infrastructure

The Infrastructure layer contains technical implementations.

It contains:

- AppDbContext
- EF Core repository implementations
- UnitOfWork implementation
- JWT token generation implementation
- Password hashing implementation
- Current user implementation
- EF Core migrations
- Infrastructure dependency injection setup

## Architecture Flow

The project follows a request/response DTO flow:

```text
Controller receives Request DTO
        ↓
Service handles business logic
        ↓
Service maps Request DTO to Entity when needed
        ↓
Repository works with Entity
        ↓
Service maps Entity to Response DTO
        ↓
Controller returns Response DTO
```

Controllers are intentionally thin. Business decisions belong in services. Database access belongs behind repository interfaces.

## Main Features

### Authentication

Implemented endpoints:

```http
POST /api/auth/register
POST /api/auth/login
```

Authentication includes:

- Password hashing with BCrypt
- JWT generation
- Role claims
- Current user extraction from JWT
- Swagger Bearer token support

Default registered users receive the `Customer` role.

### Categories

Typical category endpoints:

```http
GET    /api/categories
GET    /api/categories/{id}
POST   /api/categories
PUT    /api/categories/{id}
DELETE /api/categories/{id}
```

### Products

Typical product endpoints:

```http
GET    /api/products
GET    /api/products/{id}
GET    /api/products/by-category/{categoryId}
POST   /api/products
PUT    /api/products/{id}
DELETE /api/products/{id}
```

Products belong to categories and include price, stock quantity, description, and creation date.

### Cart

Cart endpoints require authentication.

```http
GET    /api/cart/me
POST   /api/cart
PUT    /api/cart/{cartItemId}
DELETE /api/cart/{cartItemId}
DELETE /api/cart/clear
```

Cart rules:

- User ID comes from JWT, not from the request body.
- Users can only access and modify their own cart items.
- Product stock is checked before adding or updating cart quantity.
- Adding the same product again increases the existing cart item quantity.

### Orders

Order endpoints include customer checkout and admin order management.

```http
POST /api/orders
GET  /api/orders/me
GET  /api/orders/me/{orderId}
GET  /api/orders
PUT  /api/orders/{orderId}/status
```

Order rules:

- Checkout uses the authenticated user's cart.
- Empty cart checkout is rejected.
- Product stock is checked again during checkout.
- Stock is reduced only when an order is created.
- Cart is cleared after successful checkout.
- Customers can only view their own orders.
- Admin endpoints can list all orders and update order status.

## Transactional Checkout

Checkout is handled as one database transaction.

These operations are treated as a single unit:

```text
1. Load current user's cart
2. Check product existence
3. Check stock quantity
4. Reduce product stock
5. Create order
6. Create order items
7. Clear cart
8. Commit transaction
```

If any step fails, the transaction is rolled back.

This prevents inconsistent data such as:

- stock being reduced without an order being created
- order being created while cart clearing fails
- partial checkout state being saved

The Application layer depends on an `IUnitOfWork` abstraction. The Infrastructure layer implements it using EF Core transactions.

## Error Handling

The project uses custom exceptions and global exception middleware.

Examples:

| Exception | HTTP Status |
|---|---:|
| BadRequestException | 400 |
| UnauthorizedException | 401 |
| NotFoundException | 404 |
| Unhandled Exception | 500 |

This keeps controller code cleaner and avoids returning generic 500 errors for expected business failures.

## Database

The project uses EF Core code-first migrations.

The database is generated from:

- Domain entities
- AppDbContext
- EF Core configuration
- Migrations

Typical commands:

```bash
dotnet ef migrations add InitialCreate \
  --project CleanCommerce.Infrastructure \
  --startup-project CleanCommerce.Api


dotnet ef database update \
  --project CleanCommerce.Infrastructure \
  --startup-project CleanCommerce.Api
```

## Configuration

Create or update `appsettings.json` in `CleanCommerce.Api`.

Example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=CleanCommerceDb;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Issuer": "CleanCommerce",
    "Audience": "CleanCommerceClient",
    "Key": "replace-this-with-a-long-secure-development-key"
  }
}
```

Do not use a short JWT key. For real deployment, store secrets outside source control.

## How to Run

### 1. Clone the repository

```bash
git clone https://github.com/SabaSulakvelidze/CleanCommerce.git
cd CleanCommerce
```

### 2. Restore packages

```bash
dotnet restore
```

### 3. Configure database connection

Update `CleanCommerce.Api/appsettings.json` and set your SQL Server connection string.

### 4. Apply migrations

```bash
dotnet ef database update \
  --project CleanCommerce.Infrastructure \
  --startup-project CleanCommerce.Api
```

### 5. Run the API

```bash
dotnet run --project CleanCommerce.Api
```

### 6. Open Swagger

Open the Swagger URL shown in the console, usually similar to:

```text
https://localhost:<port>/swagger
```

## Suggested Test Flow in Swagger

1. Register a new user.
2. Login and copy the JWT token.
3. Click `Authorize` in Swagger.
4. Enter the token as:

```text
Bearer your_token_here
```

5. Create categories.
6. Create products.
7. Add products to cart.
8. View current user's cart.
9. Create an order from the cart.
10. Check that stock is reduced and cart is cleared.

## Current Design Choices

### Why request/response DTOs?

The API should not expose EF Core entities directly. Request DTOs define what the client can send. Response DTOs define what the API returns.

This avoids problems such as:

- exposing internal entity relationships
- accidentally accepting fields the client should not control
- circular reference issues in JSON serialization
- tightly coupling API contracts to database models

### Why repositories?

Repositories hide EF Core queries behind interfaces. The Application layer depends on abstractions, while Infrastructure provides the actual EF Core implementation.

This supports the layered architecture goal of keeping business logic separate from database details.

### Why UnitOfWork?

EF Core `DbContext` already behaves like a unit of work, but the Application layer should not directly depend on `AppDbContext` or EF Core transaction APIs.

The `IUnitOfWork` abstraction allows checkout to control transactions while keeping EF Core inside Infrastructure.

## Limitations

This is still a learning project. Some parts are intentionally simple.

Known limitations:

- No refresh tokens yet
- No email verification
- No payment integration
- No product images
- No pagination yet
- No advanced filtering/searching yet
- No automated tests yet
- No concurrency token handling for stock updates yet
- No Docker setup yet
- Repository save behavior may still be mixed in some parts

## Good Next Improvements

Recommended next steps:

1. Add pagination for product and order listing.
2. Add product search, filtering, and sorting.
3. Make repository save behavior consistent with UnitOfWork.
4. Add EF Core concurrency handling for stock updates.
5. Add refresh tokens.
6. Add role management endpoints.
7. Add product images.
8. Add unit and integration tests.
9. Add Docker support.
10. Add GitHub Actions CI.

## Author

Saba Sulakvelidze

GitHub: `SabaSulakvelidze`

## Repository

```text
https://github.com/SabaSulakvelidze/CleanCommerce
```
