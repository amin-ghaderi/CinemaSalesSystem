# -------------------------------
# Build Stage
# -------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

# Restore presentation project only: .dockerignore excludes tests/, so restoring the full .sln would fail.
RUN dotnet restore src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj

RUN dotnet publish src/CinemaSalesSystem.Presentation/CinemaSalesSystem.Presentation.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

# -------------------------------
# Runtime Stage
# -------------------------------
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/publish .
RUN mkdir -p /app/logs
ENV DOTNET_ENVIRONMENT=Production
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["dotnet", "CinemaSalesSystem.Presentation.dll"]
