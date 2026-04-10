# CinemaSalesSystem

A **.NET 8** modular monolith for cinema sales operations, structured with **Clean Architecture** and **lightweight Domain-Driven Design (DDD)**. The solution ships a **console UI**, **EF Core** persistence (in-memory by default), **Serilog** structured logging, and a layered automated test suite.

---

## Project Overview

CinemaSalesSystem models movies, show times, ticket sales, snack orders, campaigns/discounts, and sales reporting. The **Domain** layer captures business rules and aggregates; the **Application** layer orchestrates use cases and DTOs; **Infrastructure** implements persistence and EF Core mappings; **Presentation** hosts the console host, menus, and composition root.

---

## Features

| Area | Capabilities |
|------|----------------|
| **Movies** | Manage film catalog (title, duration, rating, genre, description). |
| **Show times** | Schedule and list showings linked to movies. |
| **Tickets** | Purchase tickets for a show time with pricing and seat concepts. |
| **Snacks** | Concession lines and order integration. |
| **Campaigns** | Discount codes and promotional rules. |
| **Reports** | Sales reporting for operational insight. |
| **Logging** | Structured Serilog output to console and rolling files. |
| **Testing** | Domain and application unit tests, infrastructure integration tests, Coverlet coverage. |

---

## Architecture Overview

Dependencies point **inward**: Presentation → Application → Domain; Infrastructure → Application & Domain. The Application layer defines **abstractions** (repository interfaces, application services); Infrastructure **implements** them. The Domain layer has **no** references to outer layers.

For a deeper walkthrough, see **[docs/Architecture.md](docs/Architecture.md)**.

---

## Technology Stack

| Category | Technology |
|----------|------------|
| Runtime | .NET 8 (C#) |
| UI | Console (`Microsoft.Extensions.Hosting`) |
| ORM | Entity Framework Core 8 (In-Memory provider in current setup) |
| Logging | Serilog (Console, File, configuration from `appsettings.json`) |
| DI | `Microsoft.Extensions.DependencyInjection` |
| Unit / integration tests | xUnit, FluentAssertions, Moq |
| Coverage | Coverlet (`coverlet.collector`), optional ReportGenerator for HTML |
| Build | SDK-style projects, Central Package Management (`Directory.Packages.props`) |

---

## Solution Structure

| Path | Role |
|------|------|
| `src/CinemaSalesSystem.Domain/` | Entities, aggregates, value objects, domain services, domain events |
| `src/CinemaSalesSystem.Application/` | Use cases, DTOs, mappers, persistence abstractions |
| `src/CinemaSalesSystem.Infrastructure/` | EF Core `ApplicationDbContext`, repositories, configurations, seeding |
| `src/CinemaSalesSystem.Presentation/` | `Program.cs`, console menus/services, `appsettings.json` |
| `tests/*` | Automated tests (see [docs/ProjectStructure.md](docs/ProjectStructure.md)) |
| `docs/` | Architecture, deployment, testing, and structure documentation |

Solution file: **`CinemaSalesSystem.sln`**.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (8.0 or later compatible with the repo)
- Git (optional, for clone/contribute workflows)
- PowerShell, bash, or any shell that can run `dotnet` CLI commands

### Installation

Clone the repository and restore packages from the **repository root**:

```bash
git clone <repository-url>
cd CinemaSalesSystem
dotnet restore CinemaSalesSystem.sln
```

### Build

```bash
dotnet build CinemaSalesSystem.sln
```

---

## Running the Application

From the repository root:

```bash
dotnet run --project src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj
```

The host loads **`appsettings.json`** (copied to the output directory), registers application and infrastructure services, seeds the in-memory database, and starts the interactive console menu.

> **Note:** When stdin is redirected (for example in some automation scenarios), console APIs such as `Console.Clear()` may fail. Run in a normal terminal for full UI behavior.

---

## Running Tests

```bash
dotnet test CinemaSalesSystem.sln
```

Test assemblies include domain tests, application tests, infrastructure integration tests, and legacy `CinemaSales.UnitTests`. See **[docs/TestingGuide.md](docs/TestingGuide.md)** for strategy and project layout.

---

## Code Coverage

Collect **Cobertura** output using the shared runsettings file:

```bash
dotnet test CinemaSalesSystem.sln --settings tests/coverage.runsettings --collect:"XPlat Code Coverage"
```

Reports are written under each test project’s **`TestResults/<guid>/coverage.cobertura.xml`**. To merge into an HTML report (optional), install [ReportGenerator](https://github.com/danielpalme/ReportGenerator) and point it at those files. Details: **[docs/TestingGuide.md](docs/TestingGuide.md)**.

---

## Logging with Serilog

Serilog is configured in **`src/CinemaSalesSystem.Presentation/appsettings.json`** under the **`Serilog`** section: minimum levels, Microsoft/System overrides, enrichers (machine name, thread id), **Console** and **rolling File** sinks (`logs/cinema-sales-.log`, daily roll, retention). The composition root in **`Program.cs`** uses a bootstrap logger, then **`Host`** + **`UseSerilog`** with `ReadFrom.Configuration`.

Log file location is relative to the **process working directory** (often the presentation project directory when using `dotnet run`, or the executable folder when running from `bin/.../net8.0/`).

---

## Docker Support (Upcoming)

Container images and `Dockerfile`(s) are **planned**. The deployment guide will be updated with build args, published output layout, and environment-variable mapping for configuration when Docker support lands.

---

## CI/CD Pipeline (Upcoming)

Continuous integration (restore, build, test, optional coverage upload) and deployment pipelines are **planned**. See **[docs/DeploymentGuide.md](docs/DeploymentGuide.md)** for the intended integration points.

---

## Project Roadmap

| Phase | Focus |
|-------|--------|
| **Done (Phases 0–4)** | Core domain, application use cases, EF infrastructure, console presentation |
| **Phase 5 (in progress)** | Tests, coverage, Serilog, documentation hardening |
| **Next** | Docker, CI/CD, optional SQL Server / real persistence, API or additional UIs |

---

## Contributing Guidelines

1. **Keep boundaries**: Do not reference Infrastructure or Presentation from Domain or Application core logic.
2. **Small, focused changes**: Prefer PRs that address one concern with clear commit messages.
3. **Tests**: Add or update tests for behavioral changes; keep domain tests free of EF and UI.
4. **Style**: Match existing naming; solution uses nullable reference types and treats warnings as errors (`Directory.Build.props`).
5. **Documentation**: Update `README.md` or `docs/` when behavior or commands change materially.

---

## Additional Documentation

| Document | Description |
|----------|-------------|
| [docs/Architecture.md](docs/Architecture.md) | Layers, DDD building blocks, dependency rules, ADRs-style decisions |
| [docs/DeploymentGuide.md](docs/DeploymentGuide.md) | Build, run, logging, environment, Docker/CI previews |
| [docs/TestingGuide.md](docs/TestingGuide.md) | Unit vs integration tests, Coverlet, ReportGenerator |
| [docs/ProjectStructure.md](docs/ProjectStructure.md) | Directory reference |

---

## License

Specify a license for your distribution (for example add a `LICENSE` file at the repository root). Until then, **all rights reserved** unless the repository owner states otherwise.

---

## Author Information

Documentation and codebase maintained by the **CinemaSalesSystem** project contributors. Replace this section with maintainer names, contact links, or organization details for your fork or enterprise deployment.
