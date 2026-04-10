# Technical Documentation — CinemaSalesSystem

**Version:** v1.0.0 · **Author:** AMIN GHADERI · **Framework:** .NET 8

This document summarizes implementation details for operators and developers maintaining the system.

---

## Technology Stack

| Area | Technology |
|------|------------|
| Runtime | .NET 8 (C#), nullable reference types, implicit usings |
| UI host | `Microsoft.Extensions.Hosting`, console menus |
| Application core | Clean Architecture projects (Domain, Application, Infrastructure, Presentation) |
| Persistence | Entity Framework Core 8, InMemory provider (dev/test), Npgsql (PostgreSQL production) |
| DI | `Microsoft.Extensions.DependencyInjection` |
| Logging | Serilog (console, rolling file, configuration binding) |
| Testing | xUnit, FluentAssertions, Moq, Coverlet |
| Build | SDK-style projects, Central Package Management (`Directory.Packages.props`), `Directory.Build.props` (analyzers, `TreatWarningsAsErrors`) |
| Containers | Multi-stage Dockerfile (`sdk:8.0` → `runtime:8.0`), Docker Compose with PostgreSQL |
| CI/CD | GitHub Actions: CI (format, build, test, coverage, Docker image), release workflow on tags, CodeQL, optional Docker Hub publish |

---

## Project Structure

| Path | Role |
|------|------|
| `src/CinemaSalesSystem.Domain/` | Aggregates, entities, value objects, domain services, events |
| `src/CinemaSalesSystem.Application/` | Use cases, DTOs, mappers, repository/service abstractions |
| `src/CinemaSalesSystem.Infrastructure/` | `ApplicationDbContext`, EF configurations, repositories, migrations, seed |
| `src/CinemaSalesSystem.Presentation/` | `Program.cs`, Serilog bootstrap, `appsettings*.json`, console UI |
| `tests/` | `CinemaSalesSystem.Domain.Tests`, `.Application.Tests`, `.Infrastructure.Tests`, `CinemaSales.UnitTests` |
| `docs/` | User, technical, architecture, release, and legacy guides (`Architecture.md`, `TestingGuide.md`, …) |

Solution entry point: **`CinemaSalesSystem.sln`**.

---

## Dependency Injection

- **`Program.cs`** registers `AddApplication()` and `AddInfrastructure(IConfiguration)` after building the generic host.
- **Application** registers use cases and application services (see `DependencyInjection.cs` in Application).
- **Infrastructure** registers `DbContext` (InMemory or Npgsql from `DatabaseProvider` + connection strings), repositories, and related services.
- **Presentation** registers scoped UI-facing services (`MovieService`, `TicketService`, etc.) and **`IHostedService`** (`ConsoleApp`).
- **`ConsoleApp`** resolves scoped services via **`IServiceScopeFactory`** per menu action so `DbContext` and repositories remain scoped while the hosted service is a singleton (avoids captive dependencies).

---

## EF Core Configuration

- **`ApplicationDbContext`** lives in Infrastructure; entity configurations are in `Persistence/Configurations/`.
- **Provider selection:** `DatabaseProvider` in configuration (`InMemory` vs `PostgreSQL`).
- **Migrations:** Relational providers run `MigrateAsync()` at startup in `Program.cs` when the database is relational.
- **Seeding:** `CinemaSalesDbContextSeed` supplies workshop data; **DateTimeOffset** values for show times are stored in **UTC** for PostgreSQL/Npgsql compatibility.
- **Design-time:** `Microsoft.EntityFrameworkCore.Design` is referenced where needed for migrations from the Presentation project.

---

## Logging with Serilog

- Bootstrap logger in `Program.cs` (console, invariant culture for CA1305 compliance).
- Full pipeline: `Host` + `UseSerilog` reading **`appsettings.json`** (`Serilog` section): levels, enrichers, console and rolling file sinks (`logs/cinema-sales-.log`).
- Log path is relative to the **process working directory**.

---

## Testing Strategy

| Layer | Focus |
|-------|--------|
| **Domain** | Aggregates, value objects, domain services, invariants (no EF, no host) |
| **Application** | Use cases and services with mocked repositories (Moq) |
| **Infrastructure** | EF Core against InMemory provider, repository behavior |
| **Coverlet** | `tests/coverage.runsettings`, collector `XPlat Code Coverage`, Cobertura under `TestResults/` |

Run all tests:

```bash
dotnet test CinemaSalesSystem.sln -c Release
```

---

## CI/CD Pipeline

| Workflow | Purpose |
|----------|---------|
| **`.github/workflows/ci.yml`** | On `main` / `develop`: restore → `dotnet format --verify-no-changes` → Release build → tests with coverage → artifact upload → Docker build |
| **`.github/workflows/release.yml`** | On tag `v*.*.*` or manual dispatch: build, test, publish linux-x64 app, Docker image, save `.tar`, GitHub Release with notes |
| **`.github/workflows/docker-publish.yml`** | Push image to Docker Hub on `main` / version tags (secrets required) |
| **`.github/workflows/codeql.yml`** | Scheduled and PR C# security analysis |

---

**Author:** **AMIN GHADERI**  
© 2026 AMIN GHADERI
