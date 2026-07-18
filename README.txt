# Enterprise Backend Template

A production-oriented ASP.NET Core Web API template built with Clean Architecture (Onion-style dependency model) principles.

This project demonstrates how to structure an enterprise-grade backend application by applying modern architectural patterns, separation of concerns, and production-ready development practices.

---

# Project Goals

The purpose of this project is not to build a feature-rich application, but to provide a clean and maintainable backend foundation that can be used as a starting point for enterprise applications.

Main focus areas:

- Clean Architecture
- CQRS
- MediatR
- Repository Pattern
- Unit of Work
- Result Pattern
- FluentValidation
- Global Exception Handling
- Problem Details (RFC 7807)
- Dependency Injection
- Entity Framework Core

---

# Technologies

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- MediatR
- FluentValidation
- OpenAPI / Swagger

---

# Solution Structure

```
EnterpriseBackendTemplate

├── Domain
│
├── Contract
│
├── Application
│
├── Persistence
│
└── WebApi
```

---

# Architecture

This solution follows the Dependency Rule.

```
               WebApi
                  │
        ┌─────────┴─────────┐
        │                   │
 Application          Persistence
        │                   │
        └─────────┬─────────┘
                  │
              Contract
                  │
                  │
               Domain
```

Dependency directions are intentionally restricted.

- Domain has no external dependencies.
- Contract contains abstractions shared by Application and Persistence.
- Application never references Persistence.
- Persistence implements the abstractions defined inside Contract.
- WebApi acts only as the composition root.

---

# Architectural Decisions

## Repository Pattern

Repositories abstract persistence concerns from the Application layer.

Application depends only on interfaces.

Persistence contains all EF Core implementations.

---

## Unit of Work

All database changes are committed through a single Unit of Work.

```
Handler
    ↓
Repository
    ↓
UnitOfWork.SaveChangesAsync()
```

---

## CQRS

Commands and Queries are separated.

```
CreateProductCommand

GetProductByIdQuery

UpdateProductCommand

GetProductsQuery
```

---

## Request / Command Separation

Presentation models are intentionally separated from Application messages.

```
HTTP Request

↓

CreateProductRequest

↓

CreateProductCommand
```

This keeps HTTP contracts independent from use case contracts.

---

## Result Pattern

Application never throws exceptions for expected business failures.

Instead it returns:

```
Result

Result<T>
```

Example:

```
Product was not found.
```

instead of throwing an exception.

---

## Validation Pipeline

Validation is executed before handlers by using MediatR Pipeline Behaviors.

```
Request

↓

Validation

↓

Handler
```

---

## Global Exception Handling

Unexpected exceptions are translated into RFC 7807 Problem Details responses.

Validation failures are also returned as Problem Details.

---

## Entity Design

Entities encapsulate their own behavior.

Instead of

```csharp
product.Name = request.Name;
```

the domain exposes methods such as

```csharp
product.Update(...)
```

to protect business rules.

---

## Pagination

The project demonstrates server-side pagination.

```
Skip

Take

AsNoTracking
```

are used for efficient read operations.

---

# Implemented Features

- Create Product
- Get Product By Id
- Update Product
- Get Products (Pagination)

---

# API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | /api/products | Create Product |
| GET | /api/products/{id} | Get Product By Id |
| PUT | /api/products/{id} | Update Product |
| GET | /api/products | Get Products |

---

# Example Request

```http
POST /api/products
```

```json
{
  "name": "Mechanical Keyboard",
  "price": 2500
}
```

---

# Design Principles

- SOLID
- Separation of Concerns
- Dependency Inversion
- Composition Root
- Rich Domain Model
- Clean Architecture
- Onion Architecture

---

# Future Improvements

Possible extensions include:

- Authentication / Authorization
- Identity
- JWT
- Refresh Tokens
- Caching
- Logging
- Docker
- Integration Tests

---

# License

This project is intended for educational and portfolio purposes.