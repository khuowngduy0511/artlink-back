# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["artlink-web-api.sln", "./"]
COPY ["src/WebApi/WebApi.csproj", "src/WebApi/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/core/Application/Application.csproj", "src/core/Application/"]
COPY ["src/core/Domain/Domain.csproj", "src/core/Domain/"]

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
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Copy published files
COPY --from=publish /app/publish .

# Set environment variable
ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "WebApi.dll"]
