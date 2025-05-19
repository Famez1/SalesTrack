# Стартовое окружение
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Сборка проекта
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY src/SalesTrack.Api/SalesTrack.Api.csproj SalesTrack.Api/
COPY src/SalesTrack.Persistence/SalesTrack.Persistence.csproj SalesTrack.Persistence/
COPY src/SalesTrack.Domain/SalesTrack.Domain.csproj SalesTrack.Domain/
COPY src/SalesTrack.Application/SalesTrack.Application.csproj SalesTrack.Application/
COPY src/SalesTrack.Contracts/SalesTrack.Contracts.csproj SalesTrack.Contracts/
COPY src/SalesTrack.Common/SalesTrack.Common.csproj SalesTrack.Common/
COPY src/SalesTrack.Abstractions/SalesTrack.Abstractions.csproj SalesTrack.Abstractions/
COPY src/SalesTrack.Infrastructure/SalesTrack.Infrastructure.csproj SalesTrack.Infrastructure/

RUN dotnet restore SalesTrack.Api/SalesTrack.Api.csproj

COPY . .

WORKDIR /src/SalesTrack.Api
RUN dotnet build SalesTrack.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish SalesTrack.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesTrack.Api.dll"]
