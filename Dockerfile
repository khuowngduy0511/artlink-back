# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["artlink-web-api.sln", "./"]
COPY ["src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Migrators.MSSQL/Migrators.MSSQL.csproj", "src/Migrators.MSSQL/"]
COPY ["src/core/Application/Application.csproj", "src/core/Application/"]
COPY ["src/core/Domain/Domain.csproj", "src/core/Domain/"]
COPY ["tests/WepApiTest/WepApiTest.csproj", "tests/WepApiTest/"]
COPY ["tests/Application.Test/Application.Test.csproj", "tests/Application.Test/"]
COPY ["tests/Domain.Test/Domain.Test.csproj", "tests/Domain.Test/"]
COPY ["tests/Infrastructure.Test/Infrastructure.Test.csproj", "tests/Infrastructure.Test/"]

# Restore dependencies
RUN dotnet restore "artlink-web-api.sln"

# Copy everything else
COPY . .

# Build the application
WORKDIR "/src/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy published files
COPY --from=publish /app/publish .

# Set environment variable
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "WebApi.dll"]
