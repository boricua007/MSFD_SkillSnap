# MSFD SkillSnap

## Overview

SkillSnap is a developer portfolio application built with ASP.NET Core Web API and Blazor WebAssembly, backed by Entity Framework Core and SQLite. It models a portfolio owner (`PortfolioUser`) who has many `Project` entries and many `Skill` entries, and exposes that data through a REST API consumed by a Blazor client.

The application demonstrates the foundational data-access pipeline for a full-stack app: defining domain models, configuring a `DbContext` with one-to-many relationships, generating EF Core migrations, seeding sample data through an API endpoint, and rendering that data through Blazor components.

## Features

✅ ASP.NET Core Web API with OpenAPI (Swagger) support in Development  
✅ Entity Framework Core code-first modeling  
✅ SQLite database with migration history  
✅ One-to-many relationships: `PortfolioUser` → `Project` and `PortfolioUser` → `Skill`  
✅ `SeedController` endpoint to populate sample portfolio data  
✅ Blazor WebAssembly client with reusable, parameterized components  
✅ `ProfileCard`, `ProjectList`, and `SkillTags` components rendered on the Home page  
✅ Project and skill services connected to API endpoints  
✅ Loading, empty, and failed-request states in the data components  
✅ Swagger UI for testing GET and POST API requests  
✅ Runtime logs stored in the ignored `Logs/` folder  
✅ Clean, well-structured project layout

## Getting Started

1. Clone the repository

   ```powershell
   git clone https://github.com/boricua007/MSFD_SkillSnap.git
   cd MSFD_SkillSnap
   ```

2. Install the EF Core CLI tools (if not already installed)

   ```powershell
   dotnet tool install --global dotnet-ef
   ```

3. Apply migrations to create the database

   ```powershell
   dotnet ef database update -p MSFD_SkillSnap.Api -s MSFD_SkillSnap.Api
   ```

4. Run the API in one terminal

   ```powershell
   dotnet run --project MSFD_SkillSnap.Api --urls http://localhost:5000
   ```

5. Open Swagger UI while the API is running

   ```text
   http://localhost:5000/swagger
   ```

   Swagger UI is only enabled in the Development environment. `Properties/launchSettings.json` sets `ASPNETCORE_ENVIRONMENT=Development` by default.

6. Seed sample data

   - Call `POST /api/seed` to populate a sample `PortfolioUser` with related `Project` and `Skill` records.

7. Run the Blazor client in a second terminal

   ```powershell
   dotnet run --project MSFD_SkillSnap.Client --urls http://localhost:5001
   ```

8. Open the client

   ```text
   http://localhost:5001
   ```

   The client uses `http://localhost:5000/` as its API base address.

   When redirecting output from PowerShell, store runtime logs in `Logs/`:

   ```powershell
   Logs\api-running.log
   Logs\api-running-error.log
   Logs\client-running.log
   Logs\client-running-error.log
   ```

## API Endpoints

The controller uses the route prefix `api/[controller]`.

### Seed

| Method | Route | Description |
| --- | --- | --- |
| `POST` | `/api/seed` | Seed a sample `PortfolioUser` with related projects and skills if none exist. |

### Projects

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/projects` | Return all projects. |
| `POST` | `/api/projects` | Create a project using `ProjectCreateRequest`. |

Example request body:

```json
{
   "title": "My Test Project",
   "description": "Testing project creation from Swagger",
   "imageUrl": "https://placehold.co/300x200",
   "portfolioUserId": 1
}
```

### Skills

| Method | Route | Description |
| --- | --- | --- |
| `GET` | `/api/skills` | Return all skills. |
| `POST` | `/api/skills` | Create a skill. |

## Project Structure

```
MSFD_SkillSnap/
│
├── MSFD_SkillSnap.Api/
│   ├── Models/
│   │   ├── PortfolioUser.cs
│   │   ├── Project.cs
│   │   ├── ProjectCreateRequest.cs
│   │   └── Skill.cs
│   ├── Controllers/
│   │   └── SeedController.cs
│   ├── Migrations/
│   ├── Program.cs
│   ├── SkillSnapContext.cs
│   └── MSFD_SkillSnap.Api.csproj
│
├── MSFD_SkillSnap.Client/
│   ├── Components/
│   │   ├── ProfileCard.razor
│   │   ├── ProjectList.razor
│   │   └── SkillTags.razor
│   ├── Layout/
│   ├── Pages/
│   │   └── Home.razor
│   ├── Services/
│   │   ├── ProjectService.cs
│   │   └── SkillService.cs
│   └── MSFD_SkillSnap.Client.csproj
│
├── Logs/                    # Ignored runtime logs
├── SubmissionChecklist.txt
├── MSFD_SkillSnap.slnx
└── README.md
```

## How It Works

1. `Program.cs` registers controllers, OpenAPI services, and `SkillSnapContext` with SQLite.
2. `PortfolioUser`, `Project`, and `Skill` are defined as related entities; `SkillSnapContext.OnModelCreating` configures the one-to-many relationships (`PortfolioUser.Projects`, `PortfolioUser.Skills`) with cascade delete.
3. `SeedController` inserts a sample `PortfolioUser` with related projects and skills if none exist yet.
4. `ProjectService` and `SkillService` call the API using the registered `HttpClient`.
5. The Blazor client's `Home` page renders `ProfileCard`, `ProjectList`, and `SkillTags` components, with `ProfileCard` accepting `Name`, `Bio`, and `ImageUrl` parameters.
6. `ProjectList` and `SkillTags` display loading, empty, and API error states.
7. The Development environment enables Swagger UI for exploring and testing the API in a browser.

## Key Concepts Demonstrated

- Entity Framework Core code-first development
- One-to-many entity relationships configured via Fluent API
- Database migrations (`dotnet ef migrations add`, `dotnet ef database update`)
- SQLite as a lightweight relational database
- Dependency injection and `DbContext` scoping
- ASP.NET Core minimal hosting model
- Swagger/OpenAPI endpoint documentation
- Blazor WebAssembly component composition and parameters

## About

SkillSnap is the final full-stack capstone project for the Microsoft Full Stack Developer course and the Full-Stack Certification track. It is a .NET 10 application designed to showcase a developer's projects and skills, bringing together ASP.NET Core Web API, Entity Framework Core, SQLite, and Blazor WebAssembly.

## Author

Daisy Viruet-Allen (boricua007)

GitHub: [https://github.com/boricua007](https://github.com/boricua007)
