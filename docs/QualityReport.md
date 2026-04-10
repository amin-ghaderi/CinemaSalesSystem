# CinemaSalesSystem — Quality Report

**Date:** 2026-04-10  
**Stack:** .NET 8, Clean Architecture, xUnit, Coverlet, Docker, GitHub Actions  

This report summarizes the quality gates applied to the solution: formatting, static analysis, tests, container build, and CI integration.

---

## Code formatting

| Check | Status |
|--------|--------|
| `dotnet format CinemaSalesSystem.sln` | Applied (SDK formatter; UTF-8, LF per `.editorconfig`) |
| `dotnet format CinemaSalesSystem.sln --verify-no-changes` | **Pass** (used in CI) |

**Note:** Collection-expression returns such as `return[.. sequence]` conflicted with StyleCop SA1010 and the formatter’s `return` spacing rules. Those returns were replaced with `.ToList()` / fluent chains ending in `.ToList()` in the Application layer so format verification and analyzers agree.

---

## Static analysis

| Setting | Location |
|---------|-----------|
| `AnalysisLevel` = `latest` | `Directory.Build.props` |
| `EnableNETAnalyzers` = `true` | `Directory.Build.props` |
| `EnforceCodeStyleInBuild` = `true` | `Directory.Build.props` |
| `TreatWarningsAsErrors` = `true` | `Directory.Build.props` |
| StyleCop.Analyzers `1.2.0-beta.507` | Central Package Management + global `PackageReference` in `Directory.Build.props` |
| CA / IDE / StyleCop severities | `.editorconfig` (targeted suppressions for DDD style, docs, and host logging) |

**Build:** `dotnet build CinemaSalesSystem.sln -c Release` completes with **0 warnings, 0 errors** on the development environment used for this report.

**Domain layer:** No behavioral changes were made in Domain code for this effort; analyzer noise there is controlled via `.editorconfig` rather than large refactors.

---

## Performance observations

- **Release build:** Full solution Release build completes in the low tens of seconds on the machine used for verification (typical for a solution of this size).
- **Console host:** The Presentation project is an interactive `IHostedService` console UI; it is not suited to unattended long-run profiling in CI. For local checks, use `dotnet run --project src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj -c Release` and exercise menus manually; watch Serilog output for anomalies.
- **CA1848** (high-performance logging): Set to **none** in `.editorconfig` for this codebase so bootstrap `LogInformation` calls remain acceptable without `LoggerMessage` source generators.

---

## Test coverage summary

| Metric | Value |
|--------|--------|
| Test assemblies | 4 (Domain, Application, Infrastructure, CinemaSales.UnitTests) |
| Latest run | **18** tests, **0** failures (Release) |
| Collector | Coverlet (`XPlat Code Coverage`), settings: `tests/coverage.runsettings` |
| Artifacts | Cobertura XML under `TestResults/<guid>/coverage.cobertura.xml` per test host |

Coverage percentages are **per collector run** (not a single merged solution metric). To produce a combined HTML or overall line-rate report, use a tool such as [ReportGenerator](https://github.com/danielpalme/ReportGenerator) over all `coverage.cobertura.xml` files.

---

## Docker build verification

| Command | Result |
|---------|--------|
| `docker build -t cinemasalessystem:quality .` | **Success** (Release publish of Presentation) |

**Important:** The image build stage must include **`.editorconfig`** alongside `Directory.Build.props` / `Directory.Packages.props`. Without it, Linux builds apply stricter default StyleCop / code-style behavior than the Windows tree and can fail under `TreatWarningsAsErrors`. The Dockerfile was updated to `COPY .editorconfig .`.

An **IDE0005 / EnableGenerateDocumentationFile** interaction in container builds was addressed by setting `IDE0005` and `EnableGenerateDocumentationFile` to **none** in `.editorconfig` (see [dotnet/roslyn#41640](https://github.com/dotnet/roslyn/issues/41640)).

---

## CI/CD integration

Workflow: `.github/workflows/ci.yml`

1. Restore  
2. **`dotnet format ${{ env.SOLUTION_FILE }} --verify-no-changes`**  
3. **`dotnet build` Release** (`--no-restore`)  
4. Tests with Coverlet and artifact upload (existing)  
5. Docker image build (after tests), unchanged  

---

## Production readiness checklist

- [x] Formatter gate in CI  
- [x] Release build with warnings as errors  
- [x] Analyzers + StyleCop on all projects via shared props  
- [x] Tests green with Coverlet collection  
- [x] Docker Release image builds  
- [x] Domain business rules not modified for quality work (config / Application / Presentation / Infrastructure / Dockerfile only)  

---

## References

- Solution file: `CinemaSalesSystem.sln`  
- Global MSBuild: `Directory.Build.props`, `Directory.Packages.props`  
- Editor and analyzer rules: `.editorconfig`  
- CI: `.github/workflows/ci.yml`  
