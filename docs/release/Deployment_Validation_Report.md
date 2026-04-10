# Deployment Validation Report – CinemaSalesSystem

## Version

v1.0.0

## Author

AMIN GHADERI

## Validation Date

April 11, 2026

## Environment

- Docker
- Docker Compose
- PostgreSQL 16 (Alpine image via Compose)
- .NET 8 Runtime (application container)

## Compose reference

| Item | Value |
|------|--------|
| Application container | `cinemasalessystem` |
| PostgreSQL container | `cinemasales_postgres` |
| Database name | `cinemasales` (not `cinemasalesdb`) |
| Compose file | `docker-compose.yml` (repository root) |

## Validation procedure (executed)

1. **Optional image load:** `docker load -i artifacts/v1.0.0/docker/cinemasalessystem_v1.0.0.tar` (when the archive exists).
2. **Stack start:** `docker compose up --build -d`
3. **Containers:** `docker ps` — `cinemasalessystem` **Up**; `cinemasales_postgres` **Up (healthy)**.
4. **Application logs:** `docker logs cinemasalessystem` — contains **"CinemaSalesSystem started successfully"** and **"CinemaSales Console UI started"**.
5. **Database:** `docker exec cinemasales_postgres psql -U postgres -d cinemasales -c "\dt"` — EF Core tables present (e.g. `Movies`, `ShowTimes`, `Tickets`, `__EFMigrationsHistory`, …).
6. **Teardown:** `docker compose down`

> **Note:** The sample command using container name `postgres` and database `cinemasalesdb` does not match this repository. Use **`cinemasales_postgres`** and database **`cinemasales`** as defined in `docker-compose.yml`.

## Results

| Component | Status |
|-----------|--------|
| Docker Image | ✅ Passed |
| Docker Compose | ✅ Passed |
| Application Startup | ✅ Passed |
| Database Connectivity | ✅ Passed |
| Logging | ✅ Passed |
| Container Health | ✅ Passed |

## Conclusion

The CinemaSalesSystem has been successfully deployed and validated in a production-like Docker Compose environment with PostgreSQL.

**Designed and Developed by AMIN GHADERI**
