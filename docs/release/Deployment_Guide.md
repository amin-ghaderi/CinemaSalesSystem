# Deployment Guide — CinemaSalesSystem v1.0.0

**Author:** AMIN GHADERI · **Release Date:** April 11, 2026

---

## Local Deployment

### Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (for build/run from source)
- Optional: Docker Engine + Docker Compose v2 (for containerized PostgreSQL + app)

### Build and run (CLI)

From the repository root:

```bash
dotnet restore CinemaSalesSystem.sln
dotnet build CinemaSalesSystem.sln -c Release
dotnet run --project src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj
```

### Configuration profiles

| Environment | Typical use | Database |
|-------------|-------------|----------|
| **Development** | Local debugging | InMemory (see `appsettings.Development.json`) |
| **Production** | Docker / server | PostgreSQL (`appsettings.Production.json` or environment variables) |

Set the environment:

```bash
# Linux/macOS
export DOTNET_ENVIRONMENT=Development

# Windows (PowerShell)
$env:DOTNET_ENVIRONMENT = "Development"
```

### Published output (framework-dependent, Linux x64)

```bash
dotnet publish src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj \
  -c Release -r linux-x64 --self-contained false -o ./publish
```

On the target machine, install the **ASP.NET Core / .NET runtime 8** matching the app type (console uses the shared runtime). Run:

```bash
cd publish
export DOTNET_ENVIRONMENT=Production   # if using PostgreSQL
dotnet CinemaSalesSystem.Presentation.dll
```

---

## Docker Deployment

### Build image

```bash
docker build -t cinemasalessystem:1.0.0 .
docker tag cinemasalessystem:1.0.0 cinemasalessystem:latest
```

### Compose (application + PostgreSQL)

```bash
docker compose up --build -d
```

- **Postgres** healthcheck must pass before the app container starts (`depends_on` with `condition: service_healthy`).
- Application environment in Compose sets `DatabaseProvider=PostgreSQL` and `ConnectionStrings__PostgreSQL` to the `postgres` service.

### Load saved image (offline distribution)

```bash
docker load -i path/to/cinemasalessystem_1.0.0.tar
```

---

## GitHub Actions Release Process

1. **CI** (`.github/workflows/ci.yml`): validates every push/PR to `main` and `develop` (format, Release build, tests, coverage, Docker build).
2. **Release** (`.github/workflows/release.yml`): triggered by tag `v*.*.*` or **workflow_dispatch** — publishes the console app (linux-x64), builds Docker image, saves `cinemasalessystem_1.0.0.tar`, creates a **GitHub Release** with `docs/release/Release_Notes_v1.0.0.md`.
3. **Docker Hub** (`.github/workflows/docker-publish.yml`): optional; requires `DOCKERHUB_USERNAME` and `DOCKERHUB_TOKEN` secrets.

To cut **v1.0.0** from Git (after workflow is on `main`):

```bash
git tag v1.0.0
git push origin v1.0.0
```

---

## Environment Variables

| Variable | Purpose |
|----------|---------|
| `DOTNET_ENVIRONMENT` | `Development` \| `Production` — selects `appsettings.*.json` merge and behavior. |
| `DOTNET_RUNNING_IN_CONTAINER` | Set to `true` in Dockerfile for container-aware defaults (optional). |
| `DatabaseProvider` | `InMemory` or `PostgreSQL` — overrides JSON when set (Infrastructure reads configuration). |
| `ConnectionStrings__PostgreSQL` | Npgsql connection string (double underscore for nested config in env). |

Example (Compose-style):

```yaml
environment:
  - DOTNET_ENVIRONMENT=Production
  - DatabaseProvider=PostgreSQL
  - ConnectionStrings__PostgreSQL=Host=postgres;Port=5432;Database=cinemasales;Username=postgres;Password=postgres
```

---

## Production Considerations

- **Secrets:** Never commit real passwords; use secrets managers or Compose secrets in real deployments.
- **Migrations:** Relational databases apply EF migrations at startup; ensure the DB user can create/alter schema or run migrations in a controlled job.
- **Time zones:** Seed and persisted **timestamps** use **UTC** for PostgreSQL compatibility; display local time in UI if required.
- **Logging:** Serilog file paths are under the process working directory; mount a volume for `/app/logs` in Docker (see `docker-compose.yml`).
- **Console UI:** Requires an interactive TTY for full menu behavior; avoid headless expectations without automation changes.
- **TLS / reverse proxy:** Not applicable to the raw console host; add only if you wrap the app behind another service layer.

---

**Author:** **AMIN GHADERI**  
© 2026 AMIN GHADERI
