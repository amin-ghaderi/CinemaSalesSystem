# -------------------------------
# Stage 1: Build
# -------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY CinemaSalesSystem.sln .
COPY Directory.Build.props .
COPY Directory.Packages.props .
COPY .editorconfig .

COPY src/CinemaSalesSystem.Domain/ src/CinemaSalesSystem.Domain/
COPY src/CinemaSalesSystem.Application/ src/CinemaSalesSystem.Application/
COPY src/CinemaSalesSystem.Infrastructure/ src/CinemaSalesSystem.Infrastructure/
COPY src/CinemaSalesSystem.Presentation/ src/CinemaSalesSystem.Presentation/

# Restore and publish Presentation (transitive restore of Domain/Application/Infrastructure).
# The full solution is not restored here because test projects are excluded from the image context.
RUN dotnet restore src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj

RUN dotnet publish src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# -------------------------------
# Stage 2: Runtime
# -------------------------------
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/publish .

# Create logs directory for Serilog
RUN mkdir -p /app/logs

# Environment variables
ENV DOTNET_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true

# Entry point
ENTRYPOINT ["dotnet", "CinemaSalesSystem.Presentation.dll"]
