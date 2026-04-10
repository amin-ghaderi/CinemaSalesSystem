# Project Structure — CinemaSalesSystem

This document maps **directories** under the repository root and their **purpose**. Paths use forward slashes for readability; they are equivalent to Windows backslash paths.

---

## Repository Root

| Path | Purpose |
|------|---------|
| `CinemaSalesSystem.sln` | Visual Studio / `dotnet` solution entry point. |
| `Directory.Build.props` | Shared MSBuild properties (e.g. `net8.0`, nullable, warnings as errors, default root namespace). |
| `Directory.Packages.props` | Central Package Management — NuGet versions for all projects. |
| `README.md` | Project overview and quick start. |
| `docs/` | Architecture, deployment, testing, and structure documentation. |
| `tests/` | All test projects and shared test assets. |
| `src/` | Application source (Domain, Application, Infrastructure, Presentation). |
| `coverage-report/` | **Generated** — optional HTML output from ReportGenerator (gitignored when configured). |

---

## Source (`src/`)

### `src/CinemaSalesSystem.Domain/`

| Area | Purpose |
|------|---------|
| `Aggregates/` | Aggregate roots (e.g. `Movies/Movie`) and owned concepts. |
| `Entities/` | Entities with identity (`Snack`, `Seat`, `Ticket`, …). |
| `ValueObjects/` | Immutable value types (`Money`, `DiscountCode`, …). |
| `Enums/` | Domain enumerations (`MovieRating`, `TicketType`, …). |
| `Common/` | `Entity`, `AggregateRoot`, `ValueObject`, `Guard`, shared base types. |
| `DomainServices/` | Domain service implementations and related interfaces. |
| `Events/` | Domain event types. |
| `Exceptions/` | Domain-specific exceptions. |
| `Interfaces/` | Domain-facing interfaces (placeholders / extensions). |
| `*.md` | In-repo domain notes (e.g. ubiquitous language, model notes). |

**Rule:** No references to Application, Infrastructure, or Presentation.

---

### `src/CinemaSalesSystem.Application/`

| Area | Purpose |
|------|---------|
| `Abstractions/Persistence/` | Repository interfaces (`IMovieRepository`, …). |
| `Abstractions/Services/` | Application service interfaces consumed by Presentation. |
| `Commands/` | Command types driving writes. |
| `Queries/` | Query types for reads. |
| `UseCases/` | Application use case classes (orchestration). |
| `DTOs/` | Data transfer objects for boundaries. |
| `Mappings/` | Mapping between domain and DTOs. |
| `Services/` | Application service implementations. |
| `Pricing/` | Pricing-related application logic helpers. |
| `Common/` | Shared application helpers, placeholders. |
| `DependencyInjection.cs` | `AddApplication()` extension. |

**Rule:** Depends only on **Domain**; defines contracts implemented by Infrastructure.

---

### `src/CinemaSalesSystem.Infrastructure/`

| Area | Purpose |
|------|---------|
| `Persistence/Context/` | `ApplicationDbContext`. |
| `Persistence/Configurations/` | EF Core `IEntityTypeConfiguration<>` mappings. |
| `Persistence/Seed/` | `CinemaSalesDbContextSeed` and related seed data. |
| `Persistence/` (other) | Storage helpers for value conversions where used. |
| `Repositories/` | Concrete repository implementations. |
| `DependencyInjection.cs` | `AddInfrastructure(IConfiguration)` — DbContext, repositories. |

**Rule:** Implements Application abstractions; references **Application** and **Domain**.

---

### `src/CinemaSalesSystem.Presentation/`

| Area | Purpose |
|------|---------|
| `Program.cs` | Host bootstrap, Serilog, service registration, database seed, `RunAsync`. |
| `appsettings.json` | Serilog and host configuration (copied to output). |
| `Menus/` | Console menus (`MainMenu`, `MovieMenu`, …). |
| `Services/` | UI-layer services wrapping application use cases / queries. |
| `Helpers/` | Console input/output helpers. |
| `Constants/` | Menu option constants. |
| `Models/` | View models for console display. |

**Rule:** Composition root: references **Application** and **Infrastructure**; no business rules in UI helpers beyond presentation.

---

## Tests (`tests/`)

| Project | Purpose |
|---------|---------|
| `CinemaSalesSystem.Domain.Tests/` | Domain unit tests (`Entities/`, `ValueObjects/`, …). |
| `CinemaSalesSystem.Application.Tests/` | Application use case tests with mocks (`UseCases/`). |
| `CinemaSalesSystem.Infrastructure.Tests/` | EF In-Memory integration tests (`Common/TestDbContextFactory`, `Repositories/`). |
| `CinemaSales.UnitTests/` | Additional unit tests (legacy / cross-cutting). |
| `coverage.runsettings` | Coverlet Cobertura settings for `dotnet test --settings`. |

---

## Solution Folders (Visual Studio)

The `.sln` file groups projects under virtual folders **`src`** and **`tests`** for IDE navigation; on disk, the layout matches `src/` and `tests/` as above.

---

## Related Documents

- [Architecture.md](Architecture.md) — dependency rules between layers  
- [TestingGuide.md](TestingGuide.md) — how each test project is used  
- [DeploymentGuide.md](DeploymentGuide.md) — build output locations  
