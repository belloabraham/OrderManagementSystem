# 📦 Order Management System API

A robust, scalable, and performance-optimized Web API built with **.NET 8**, designed to manage customer orders, track analytics, and apply dynamic discounts through asynchronous background processing.

## 🔧 Project Structure

```
OrderManagementSystem.sln
│
├── OrderManagementSystem.API          # Entry point: ASP.NET Core Web API
├── OrderManagementSystem.Domain       # Domain entities, enums, interfaces, DTOs
├── OrderManagementSystem.Application  # Application services, business logic, AutoMapper, validation
├── OrderManagementSystem.Infrastructure # EF Core, repositories, background services
└── OrderManagementSystem.Tests        # Unit tests (coming soon)
```

## 🚀 Features

- ✅ Clean **Onion Architecture** with separation of concerns
- ✅ Modular project with **Domain**, **Application**, **Infrastructure**, and **API** layers
- ✅ RESTful endpoints for Orders, OrderItems, and Analytics
- ✅ **Background processing** for Discount logic using `Channel<T>` and `BackgroundService`
- ✅ Comprehensive **Swagger** UI with endpoint summaries and XML doc integration
- ✅ Global exception handling with detailed error messages via `ProblemDetails`
- ✅ Input validation with **FluentValidation**
- ✅ Performance-optimized: Create Order response time improved by **~1s** using background services
- ✅ Multiple environment configs: `appsettings.Prod.json`, `appsettings.Local.json`, `appsettings.Dev.json`
- ✅ Benchmarking using **BenchmarkDotNet**

## 🛠️ Technologies Used

- **.NET 8 Web API**
- **Entity Framework Core**
- **FluentValidation**
- **Swagger (Swashbuckle)**
- **Channel<T> & BackgroundService** (for async discount processing)
- **BenchmarkDotNet** (for performance measurement)
- **AutoMapper** (for mapping between domain models and DTOs)
- **Serilog / Microsoft Logging** (logging abstraction)
- **XUnit / MSTest** (unit test placeholder)

## 📊 Performance Optimization

> **Problem:**
> Discount calculation and DB updates during order creation caused latency in the `/orders` POST endpoint.

> **Solution:**
> Refactored the discount logic to a background service (`DiscountBackgroundService`) using `Channel<DiscountRequest>`. This allowed the endpoint to return instantly after saving the order, and queue the discount logic for async processing.

> **Result:**
> Measured with [BenchmarkDotNet](https://github.com/dotnet/BenchmarkDotNet), the refactor **reduced response time by nearly 1 second**, improving both user experience and throughput.

## 📌 Endpoints Overview

| HTTP Method | Endpoint                  | Description                      |
| ----------- | ------------------------- | -------------------------------- |
| `GET`       | `/api/orders`             | Fetch all orders (paginated)     |
| `POST`      | `/api/orders`             | Create a new order               |
| `PUT`       | `/api/orders/{id}/status` | Update order status              |
| `GET`       | `/api/orders/{id}/items`  | Get order items by order ID      |
| `GET`       | `/api/order-items`        | Get all order items              |
| `GET`       | `/api/order-items/{id}`   | Get a single order item          |
| `GET`       | `/api/analytics`          | Fetch analytics data (paginated) |

## 🧪 Setup & Run

1. **Clone the repo**

   ```bash
   git clone https://github.com/belloabraham/OrderManagementSystem.git
   cd OrderManagementSystem
   ```

2. **Setup DB Connection**
   Modify `appsettings.Local.json` with your database connection string (e.g. `smoothstack` as password).

3. **Run Migrations** (if using EF Core migrations)

   ```bash
   dotnet ef database update --project OrderManagementSystem.Infrastructure
   ```

4. **Run the API**

   ```bash
   dotnet run --project OrderManagementSystem.API
   ```

5. **View Swagger UI**
   Navigate to: [https://localhost:5001/swagger](https://localhost:5001/swagger)

## 🧠 Design Philosophy

- Fast, testable, and maintainable API architecture
- Strong separation between business logic, infrastructure, and controllers
- Asynchronous and background processing for performance
- Developer-friendly tooling and documentation

## 🙌 Acknowledgments

- Based on best practices and guidance from the .NET community
- Swagger documentation patterns inspired by Rahul and Publeecity internal guides
