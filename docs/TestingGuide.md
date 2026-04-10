# Testing Guide — CinemaSalesSystem

This document describes the **testing strategy**, **projects**, **how to run tests**, and **how to collect code coverage** for CinemaSalesSystem.

---

## Testing Strategy

| Goal | Approach |
|------|----------|
| Protect **domain rules** | Unit tests against entities, value objects, and domain services **without** EF or web/console hosts. |
| Protect **use cases** | Application tests with **mocked** repositories (Moq) and FluentAssertions. |
| Protect **persistence wiring** | Infrastructure integration tests with **EF Core In-Memory** and real `ApplicationDbContext` + repository implementations. |
| Regression safety | Full solution `dotnet test` in CI and before merges. |

**Clean Architecture alignment:** Domain and Application test projects **must not** reference Infrastructure or Presentation. The Infrastructure test project references Infrastructure (and transitively Application/Domain) to validate repositories and `DbContext` behavior.

---

## Unit Testing

### Domain (`tests/CinemaSalesSystem.Domain.Tests`)

- **Framework:** xUnit, FluentAssertions  
- **References:** `CinemaSalesSystem.Domain` only  
- **Examples:** `Movie` invariants, `Money` behavior  

### Application (`tests/CinemaSalesSystem.Application.Tests`)

- **Framework:** xUnit, FluentAssertions, Moq  
- **References:** Application + Domain  
- **Pattern:** Mock `IMovieRepository`, `ISnackRepository`, etc.; assert use case outcomes and interactions  

### Legacy (`tests/CinemaSales.UnitTests`)

- Additional unit coverage retained for historical scenarios; run as part of the full solution test run.

---

## Integration Testing

### Infrastructure (`tests/CinemaSalesSystem.Infrastructure.Tests`)

- **Framework:** xUnit, FluentAssertions, EF Core In-Memory  
- **References:** Infrastructure, Application, Domain  
- **Pattern:** `TestDbContextFactory` builds `ApplicationDbContext` with `UseInMemoryDatabase(Guid.NewGuid().ToString())` for **isolation** between tests.  
- **Examples:** `MovieRepository` add/list/get scenarios against a real `DbContext`.

---

## Code Coverage with Coverlet

All test projects include **`coverlet.collector`** (versions centralized in `Directory.Packages.props`).

### Cobertura collection

From the repository root:

```bash
dotnet test CinemaSalesSystem.sln --settings tests/coverage.runsettings --collect:"XPlat Code Coverage"
```

### Runsettings highlights (`tests/coverage.runsettings`)

| Setting | Effect |
|---------|--------|
| `Format` | `cobertura` |
| `Exclude` | xUnit assemblies, `*.Tests` assemblies |
| `ExcludeByAttribute` | `Obsolete`, `GeneratedCode`, `CompilerGenerated` |
| `ExcludeByFile` | `**/Migrations/*` |

### Where reports are written

After the run, Cobertura files are typically under:

`tests/<TestProject>/TestResults/<guid>/coverage.cobertura.xml`

Paths may vary slightly by SDK version; search for **`coverage.cobertura.xml`** under `TestResults` if needed.

---

## Running Tests

### Full solution

```bash
dotnet build CinemaSalesSystem.sln
dotnet test CinemaSalesSystem.sln
```

### Single project

```bash
dotnet test tests/CinemaSalesSystem.Domain.Tests/CinemaSalesSystem.Domain.Tests.csproj
```

### Filter (xUnit trait / name)

Use `dotnet test --filter "FullyQualifiedName~MovieRepository"` when focusing on one class (adjust pattern as needed).

---

## Generating Coverage Reports

### HTML (optional) — ReportGenerator

Install the global tool (once per machine):

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

After collecting Cobertura files:

```bash
reportgenerator "-reports:**/TestResults/**/coverage.cobertura.xml" "-targetdir:coverage-report" "-reporttypes:Html"
```

Open **`coverage-report/index.html`** in a browser.

> **Note:** Add `coverage-report/` and `TestResults/` to `.gitignore` (already common practice) so generated artifacts are not committed.

---

## Quality Checklist (Phase 5)

- [ ] `dotnet build CinemaSalesSystem.sln` succeeds with no warnings (solution uses `TreatWarningsAsErrors`).  
- [ ] `dotnet test CinemaSalesSystem.sln` — all tests green.  
- [ ] Coverage command runs without errors; Cobertura XML produced for CI upload if required.  

---

## Related Documents

- [Architecture.md](Architecture.md) — why layers are tested separately  
- [ProjectStructure.md](ProjectStructure.md) — test project locations  
- [DeploymentGuide.md](DeploymentGuide.md) — CI preview for test stage  
