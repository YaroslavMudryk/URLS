# Базовий імідж для запуску програми
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Побудова проєкту
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["URLS.Api/URLS.Api.csproj", "URLS.Api/"]
RUN dotnet restore "./URLS.Api/URLS.Api.csproj"
COPY . .
WORKDIR "/src/URLS.Api"
RUN dotnet build "./URLS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Публікація проєкту
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./URLS.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Фінальний контейнер
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "URLS.Api.dll"]
