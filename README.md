# Teachers Portal API

A backend API for managing teachers, students, courses, and their relationships, built using ASP.NET Core and Entity Framework Core with a clean architecture separating API and infrastructure layers.

---

## Features

- Manage Students, Teachers, Courses
- Assign courses to teachers and students
- Seed default data for quick setup
- Entity Framework Core for data access with code-first migrations
- Separate Infrastructure project for DbContext and data access logic
- Swagger/OpenAPI documentation support

---



## Technologies

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core
- SQL Server (or other EF Core compatible databases)
- Swashbuckle for Swagger/OpenAPI
- Dependency Injection and Configuration using appsettings.json

---

**Physical DB store**
Run `Add-Migrations`
Run `update-database`

## Getting Started

### Prerequisites

- [.NET SDK 8.0 or later](https://dotnet.microsoft.com/download)
- SQL Server or other supported database
- IDE like Visual Studio 2022 or VS Code

### Setup

1. **Clone the repository**

```bash
git clone https://github.com/yourusername/TeachersPortal.git
cd TeachersPortal
