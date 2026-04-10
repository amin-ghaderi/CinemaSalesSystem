# CinemaSalesSystem

**Version v1.0.0** · **.NET 8** · **Author: AMIN GHADERI**

A modular monolith for cinema sales operations, structured with **Clean Architecture** and **lightweight Domain-Driven Design (DDD)**. The solution ships a **console UI**, **EF Core** persistence (**in-memory** for local development, **PostgreSQL** for production/Docker), **Serilog** structured logging, and a layered automated test suite.

**Repository:** [github.com/amin-ghaderi/CinemaSalesSystem](https://github.com/amin-ghaderi/CinemaSalesSystem)

---

## Project Overview

CinemaSalesSystem models movies, show times, ticket sales, snack orders, campaigns/discounts, and sales reporting. The **Domain** layer captures business rules and aggregates; the **Application** layer orchestrates use cases and DTOs; **Infrastructure** implements persistence and EF Core mappings; **Presentation** hosts the console host, menus, and composition root.

| Area | Capabilities |
|------|----------------|
| **Movies** | Film catalog (title, duration, rating, genre, description). |
| **Show times** | Schedule and list showings linked to movies. |
| **Tickets** | Purchase tickets for a show time with pricing and seats. |
| **Snacks** | Concession lines and order integration. |
| **Campaigns** | Discount codes and promotional rules. |
| **Reports** | Sales reporting. |
| **Logging** | Serilog → console and rolling files. |
| **Testing** | Domain, application, and infrastructure tests; Coverlet coverage. |

---

## Architecture

Dependencies point **inward**: **Presentation** → **Application** and **Infrastructure** (composition root); **Infrastructure** → **Application** & **Domain**; **Application** → **Domain**; **Domain** has no references to outer layers.

Detailed diagrams and DDD mapping:

| Document | Description |
|----------|-------------|
| [docs/architecture/Architecture_Documentation.md](docs/architecture/Architecture_Documentation.md) | Layers, dependency flow, DDD (v1.0.0) |
| [docs/Architecture.md](docs/Architecture.md) | Extended architecture narrative |

---

## Installation

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Git (to clone the repository)
- Optional: [Docker](https://docs.docker.com/get-docker/) + Docker Compose v2 for PostgreSQL + app stack

### Clone and restore

```bash
git clone https://github.com/amin-ghaderi/CinemaSalesSystem.git
cd CinemaSalesSystem
dotnet restore CinemaSalesSystem.sln
```

### Build

```bash
dotnet build CinemaSalesSystem.sln -c Release
```

---

## Usage Guide

### Run with .NET CLI (repository root)

```bash
dotnet run --project src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj
```

Use **Development** for in-memory data (default for local runs). For PostgreSQL, set `DOTNET_ENVIRONMENT=Production` and configure connection strings (see deployment guide).

> **Note:** Run in a normal terminal. Redirected stdin or headless environments may fail when the UI calls `Console.Clear()`.

### Run with Docker

```bash
docker compose up --build
```

PostgreSQL must become **healthy** before the app container starts. Use an interactive session for the console menu.

Full user-facing steps: **[docs/user/User_Guide.md](docs/user/User_Guide.md)**

---

## Docker

- **Dockerfile:** multi-stage build (SDK → runtime), publishes the Presentation project.
- **docker-compose.yml:** PostgreSQL 16 + application, volumes for data and logs.
- **Build:** `docker build -t cinemasalessystem:1.0.0 .`

See **[docs/release/Deployment_Guide.md](docs/release/Deployment_Guide.md)** for publish layouts, environment variables, and production notes.

---

## CI/CD

| Workflow | Trigger | Purpose |
|----------|---------|---------|
| [`.github/workflows/ci.yml`](.github/workflows/ci.yml) | `main`, `develop` (push/PR) | Restore, format check, Release build, tests + coverage, Docker build |
| [`.github/workflows/release.yml`](.github/workflows/release.yml) | Tag `v*.*.*`, `workflow_dispatch` | Publish linux-x64 app, Docker image `.tar`, GitHub Release |
| [`.github/workflows/docker-publish.yml`](.github/workflows/docker-publish.yml) | `main`, tags `v*.*.*` | Docker Hub push (requires secrets) |
| [`.github/workflows/codeql.yml`](.github/workflows/codeql.yml) | Schedule, `main` PR/push | CodeQL security analysis |

---

## Testing and coverage

```bash
dotnet test CinemaSalesSystem.sln -c Release
```

```bash
dotnet test CinemaSalesSystem.sln --settings tests/coverage.runsettings --collect:"XPlat Code Coverage"
```

Details: **[docs/TestingGuide.md](docs/TestingGuide.md)**

---

## Documentation index (v1.0.0)

| Path | Audience |
|------|----------|
| [docs/user/User_Guide.md](docs/user/User_Guide.md) | End users / operators |
| [docs/technical/Technical_Documentation.md](docs/technical/Technical_Documentation.md) | Developers |
| [docs/architecture/Architecture_Documentation.md](docs/architecture/Architecture_Documentation.md) | Architects |
| [docs/release/Deployment_Guide.md](docs/release/Deployment_Guide.md) | Deployment |
| [docs/release/Release_Notes_v1.0.0.md](docs/release/Release_Notes_v1.0.0.md) | Release notes |
| [docs/release/Final_Delivery_Report.md](docs/release/Final_Delivery_Report.md) | Delivery summary |
| [docs/DeploymentGuide.md](docs/DeploymentGuide.md) | Legacy deployment reference |
| [docs/ProjectStructure.md](docs/ProjectStructure.md) | Directory layout |

---

## Technology stack

| Category | Technology |
|----------|------------|
| Runtime | .NET 8 (C#) |
| UI | Console (`Microsoft.Extensions.Hosting`) |
| ORM | EF Core 8 (InMemory, Npgsql/PostgreSQL) |
| Logging | Serilog |
| DI | `Microsoft.Extensions.DependencyInjection` |
| Tests | xUnit, FluentAssertions, Moq, Coverlet |
| Packages | Central Package Management (`Directory.Packages.props`) |

---

## Contributing

1. Keep **Clean Architecture** boundaries (no Infrastructure/Presentation references from Domain/Application core).
2. Add or update **tests** for behavior changes.
3. Solution uses **nullable**, **warnings as errors**, and **analyzers** (`Directory.Build.props`).
4. Update **docs** when commands or deployment behavior change.

---

## 👨‍💻 Author

**AMIN GHADERI** — designed and developed CinemaSalesSystem.

## 📦 Version

**v1.0.0** (April 11, 2026)

---

## License

Licensed under the **MIT License** — see [LICENSE](LICENSE).
