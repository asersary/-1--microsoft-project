# Products API

A RESTful API built with ASP.NET Core Minimal APIs for managing products (full CRUD).

## Features
- CRUD endpoints with proper HTTP status codes (200, 201, 204, 400, 404)
- Input validation
- Service layer with Dependency Injection (`IProductService`)
- Global exception handling middleware
- Structured logging with `ILogger`
- JSON file persistence
- Swagger / OpenAPI documentation

## Tech Stack
C#, ASP.NET Core, Swagger

## Getting Started
1. Clone the repo
2. Run `dotnet run`
3. Open `/swagger` to try the endpoints

## Endpoints
| Method | Route | Description |
|---|---|---|
| GET | /products | Get all products |
| GET | /products/{id} | Get a product by id |
| POST | /products | Create a product |
| PUT | /products/{id} | Update a product |
| DELETE | /products/{id} | Delete a product |
