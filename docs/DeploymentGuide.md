# Deployment Guide — CinemaSalesSystem

This guide covers **building**, **running**, and **configuring** the application for local and future production-style deployments. **Docker** and **CI/CD** sections are **preview** placeholders until those assets exist in the repository.

---

## Build Instructions

From the **repository root** (where `CinemaSalesSystem.sln` lives):

```bash
dotnet restore CinemaSalesSystem.sln
dotnet build CinemaSalesSystem.sln -c Release
```

Release output for the console app:

`src/CinemaSalesSystem.Presentation/bin/Release/net8.0/`

**Requirements:** .NET 8 SDK for build; .NET 8 runtime for running published output (or self-contained publish if you add `-r` and `--self-contained` later).

### Publish (framework-dependent example)

```bash
dotnet publish src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj -c Release -o ./publish
```

Deploy the contents of `./publish` together with **`appsettings.json`** (or environment-specific overrides).

---

## Running the Application

### Development

```bash
dotnet run --project src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj
```

`dotnet run` sets the working directory to the **project folder** by default, so relative paths (e.g. Serilog file sink `logs/...`) resolve from that context unless you change directory.

### Production-style (published folder)

```bash
cd publish
./CinemaSalesSystem.Presentation   # Linux/macOS
.\CinemaSalesSystem.Presentation.exe  # Windows
```

Ensure **`appsettings.json`** is beside the executable (it is included when copied from the project via `CopyToOutputDirectory`).

---

## Logging Configuration

Logging is driven by **`src/CinemaSalesSystem.Presentation/appsettings.json`** → **`Serilog`** section.

| Setting area | Purpose |
|--------------|---------|
| `MinimumLevel` | Default `Information`; `Microsoft` / `System` at `Warning` to reduce noise. |
| `Enrich` | `FromLogContext`, `WithMachineName`, `WithThreadId`. |
| `WriteTo` | Console + rolling file (`logs/cinema-sales-.log`, daily, 7 files retained). |
| `Properties` | e.g. `Application` = `CinemaSalesSystem`. |

### Environment-specific configuration

Use the standard .NET configuration pattern:

- Add **`appsettings.Production.json`** (or `Development`) next to the project file.
- Set **`DOTNET_ENVIRONMENT`** or **`ASPNETCORE_ENVIRONMENT`** as appropriate for the host.

`Host.CreateDefaultBuilder` loads optional environment-specific JSON automatically when present.

### Secret and connection strings (future)

When moving from in-memory to SQL Server or other providers, prefer:

- **User secrets** (development), or  
- **Environment variables** / secret stores (production),  

and bind them in `AddInfrastructure` (or future configuration types). **Do not** commit secrets to the repository.

---

## Docker Deployment (Preview)

**Status:** Planned. A typical future layout:

1. **Multi-stage Dockerfile**: SDK image → `dotnet publish` → slim runtime image.  
2. **Working directory** inside the container set to the app folder so log paths remain predictable.  
3. **Volume mount** for `logs/` if file logs must survive container restarts.  
4. **Environment variables** to override Serilog levels and connection strings.

Example sketch (not yet in repo):

```dockerfile
# PLACEHOLDER — add when Docker is implemented
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish src/CinemaSalesSystem.Presentation -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./CinemaSalesSystem.Presentation"]
```

---

## CI/CD Integration (Preview)

**Status:** Planned. Recommended pipeline stages:

| Stage | Command / action |
|-------|-------------------|
| Restore | `dotnet restore CinemaSalesSystem.sln` |
| Build | `dotnet build CinemaSalesSystem.sln -c Release --no-restore` |
| Test | `dotnet test CinemaSalesSystem.sln -c Release --no-build` |
| Coverage (optional) | `dotnet test ... --settings tests/coverage.runsettings --collect:"XPlat Code Coverage"` |

Publish artifacts from `dotnet publish` output; attach Cobertura XML to your quality gate or coverage dashboard.

---

## Environment Configuration

| Variable / mechanism | Typical use |
|---------------------|-------------|
| `DOTNET_ENVIRONMENT` | `Development` / `Staging` / `Production` for config file selection. |
| Future: connection strings | EF Core provider (SQL Server, PostgreSQL, etc.). |
| Serilog overrides | Can be extended with `Serilog:MinimumLevel:Override` per namespace in JSON. |

---

## Related Documents

- [Architecture.md](Architecture.md) — layer boundaries  
- [TestingGuide.md](TestingGuide.md) — validation before release  
- [ProjectStructure.md](ProjectStructure.md) — where files live  
