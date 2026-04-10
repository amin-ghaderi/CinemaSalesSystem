# CinemaSalesSystem – User Guide

**Version:** v1.0.0 · **Author:** AMIN GHADERI

## Overview

CinemaSalesSystem is a console-based application designed to manage cinema ticket sales efficiently.

## Features

- Movie and screening management
- Ticket booking and sales
- Snack sales and campaigns
- Reporting capabilities
- PostgreSQL database support (production) or in-memory store (development)
- Docker deployment

## Running the Application

### Prerequisites

Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0). From the **repository root**, restore and run.

### Using .NET CLI

```bash
dotnet restore CinemaSalesSystem.sln
dotnet run --project src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj
```

For **in-memory** data and local menus, use the default **Development** profile (or ensure `DatabaseProvider` is `InMemory` in configuration). For **PostgreSQL**, set `DOTNET_ENVIRONMENT=Production` and a valid connection string (see [Deployment_Guide.md](../release/Deployment_Guide.md)).

### Using Docker

From the repository root:

```bash
docker compose up --build
```

Wait until PostgreSQL is **healthy**, then use the application container’s interactive console (TTY). Press **0** to exit the main menu.

> **Tip:** Run `docker compose` in a real terminal. Redirected or non-interactive sessions may fail when the UI calls `Console.Clear()`.

## Main Menu

1. Movies Management  
2. ShowTimes Management  
3. Purchase Ticket  
4. Purchase Snack  
5. Campaigns and Discounts  
6. Sales Reports  
0. Exit  

## Further Reading

| Document | Purpose |
|----------|---------|
| [Technical_Documentation.md](../technical/Technical_Documentation.md) | Stack, configuration, testing |
| [Deployment_Guide.md](../release/Deployment_Guide.md) | Environments and production |
| [Architecture_Documentation.md](../architecture/Architecture_Documentation.md) | Layers and DDD |

---

**Author:** Designed and Developed by **AMIN GHADERI**  
© 2026 AMIN GHADERI
