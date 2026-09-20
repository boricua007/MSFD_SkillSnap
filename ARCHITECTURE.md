# SkillSnap Architecture

SkillSnap is a Blazor WebAssembly client backed by an ASP.NET Core Web API. The API uses EF Core and SQLite for application data, ASP.NET Identity with JWTs for authentication, and `IMemoryCache` for project query results.

## Architecture Diagram

The diagram separates the user-facing client from the API runtime and its
persistence services. Solid arrows represent request or data flow; dashed
arrows represent authentication and development-time tooling.

```mermaid
flowchart LR
    User([Portfolio visitor])

    subgraph Client[Blazor WebAssembly client]
        UI[Portfolio UI<br/>Home, Login, Register]
        ClientServices[Client services<br/>ProjectService, SkillService, AuthService]
        Session[(UserSessionService<br/>current user and project)]
    end

    subgraph API[ASP.NET Core API]
        Routes[HTTP endpoints<br/>Auth, Projects, Skills, Seed]
        DTOs[DTO boundary<br/>ProjectDto, SkillDto]
        Cache[(IMemoryCache<br/>project results)]
        Auth[JWT authentication<br/>ASP.NET Identity]
    end

    subgraph Data[Persistence layer]
        EF[EF Core<br/>SkillSnapContext]
        SQLite[(SQLite<br/>application and Identity data)]
        JoinTable[ProjectSkills<br/>many-to-many join table]
    end

    subgraph Tooling[Development and verification]
        Swagger[Swagger / OpenAPI]
        Logs[Runtime logging<br/>cache hit, miss, duration]
    end

    User --> UI
    UI --> ClientServices
    UI --> Session
    ClientServices -->|HTTP / JSON| Routes
    Routes --> DTOs
    Routes -->|read-through lookup| Cache
    Cache -->|cache miss| EF
    DTOs -->|flat responses| ClientServices
    Routes --> Auth
    Auth --> SQLite
    EF --> SQLite
    EF --> JoinTable
    Swagger -.->|explore and test| Routes
    Routes -.-> Logs

    classDef client fill:#e8f3ff,stroke:#1672c4,stroke-width:2px,color:#12304a;
    classDef api fill:#eaf7ef,stroke:#2b8a57,stroke-width:2px,color:#173b29;
    classDef data fill:#fff3df,stroke:#d88918,stroke-width:2px,color:#4b2d08;
    classDef tool fill:#f3edff,stroke:#7755b5,stroke-width:2px,color:#30204d;
    classDef actor fill:#ffffff,stroke:#555555,stroke-width:2px,color:#222222;

    class UI,ClientServices,Session client;
    class Routes,DTOs,Cache,Auth api;
    class EF,SQLite,JoinTable data;
    class Swagger,Logs tool;
    class User actor;
```

## Layer Responsibilities

- **Blazor WebAssembly client:** Renders the portfolio interface and communicates with the API through typed client services.
- **Client services:** `ProjectService` and `SkillService` handle API requests; `AuthService` manages authentication; `UserSessionService` stores client session state.
- **API controllers:** Expose authentication, project, skill, and seed endpoints.
- **DTOs:** Shape flat API responses and prevent EF navigation graphs from being serialized directly.
- **In-memory cache:** Stores project DTO results with sliding and absolute expiration policies.
- **EF Core:** Uses `SkillSnapContext` to map portfolio entities, Identity tables, and the `ProjectSkills` many-to-many join table.
- **SQLite:** Persists application and Identity data locally.

## Main Data Relationships

- One `PortfolioUser` can have many `Project` records.
- One `PortfolioUser` can have many `Skill` records.
- A `Project` can have many `Skill` records, and a `Skill` can belong to many `Project` records through `ProjectSkills`.
