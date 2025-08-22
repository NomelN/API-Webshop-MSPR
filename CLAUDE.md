# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is a .NET 6 Web API for a webshop application that provides authentication and CRUD operations for customers and products. The API uses JWT authentication and connects to a MockAPI service for data retrieval.

## Development Commands

### Build and Run
```bash
# Build the solution
dotnet build API-Webshop-MSPR.sln

# Run the application (from API-Webshop-MSPR directory)
cd API-Webshop-MSPR
dotnet run

# Run in development mode with hot reload
dotnet watch run
```

### Testing
```bash
# Run all tests
dotnet test

# Run specific test file
dotnet test --filter AuthenticationTests
dotnet test --filter JwtAuthenticationTests
```

### Package Management
```bash
# Restore packages
dotnet restore

# Add new package
dotnet add package PackageName
```

## Architecture

### Project Structure
- **Controllers/**: API controllers handling HTTP requests
  - `AuthenticationController`: JWT login functionality
  - `CustomersController`: Customer management and order retrieval
  - `ProductsController`: Product catalog operations
- **Services/**: Business logic and authentication services
  - `IJwtAuthenticationService`/`JwtAuthenticationService`: JWT token generation and user authentication
- **Models/**: Data models (Customers, Products, Orders, etc.)
- **Tests/**: Unit tests for authentication functionality

### Authentication Flow
- JWT-based authentication using symmetric key encryption
- Hardcoded user credentials in `JwtAuthenticationService` for demo purposes
- All API endpoints (except login) require JWT authorization
- JWT key configured in `appsettings.json`

### External Dependencies
- **MockAPI**: All data is retrieved from `https://615f5fb4f7254d0017068109.mockapi.io/api/v1/`
- **Swagger**: API documentation available at `/swagger` in development
- **Newtonsoft.Json**: JSON serialization/deserialization

### Key Patterns
- Controllers use dependency injection for services
- All data operations are asynchronous with HttpClient
- Dynamic object handling for flexible API responses
- Standard ASP.NET Core middleware pipeline (HTTPS, Authentication, Authorization, Routing)

## Configuration

### JWT Settings
JWT configuration is stored in `appsettings.json`:
```json
{
  "Jwt": {
    "Key": "hY1YOoglO74S7V325EKjv1wkwfLLBJKS"
  }
}
```

### API Routes
Base route pattern: `api/webshop/[controller]`
- Authentication: `POST /api/webshop/authentication/login`
- Customers: `GET /api/webshop/customers`
- Products: `GET /api/webshop/products`

## Development Notes

- Target Framework: .NET 6.0
- Uses traditional Startup.cs pattern (not minimal APIs)
- Error handling returns localized French error messages
- No database - all data comes from external MockAPI
- Test framework: MSTest with xUnit and Moq for mocking