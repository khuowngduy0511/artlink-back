using Application;
using Application.AppConfigurations;
using Application.Commons;
using Application.Services.ELK;
using Infrastructure;
using System.Text.Json.Serialization;
using WebApi;
using WebApi.Extensions;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(opt
    => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocumentation(); // Add Swagger config

// Bind AppConfiguration from configuration
var config = builder.Configuration.Get<AppConfiguration>();
if (config == null)
{
    config = new AppConfiguration();
    builder.Configuration.Bind(config);
}

// 🔍 DEBUG: Log SecretKey metadata to verify env vars are loaded (without exposing the full value)
var secretKey = config?.JwtConfiguration?.SecretKey ?? string.Empty;
var issuer = config?.JwtConfiguration?.Issuer ?? "(null)";
var keyPreview = secretKey.Length >= 10 ? secretKey[..10] : secretKey;

Console.WriteLine($"[CONFIG] JWT Issuer: {issuer}");
Console.WriteLine($"[CONFIG] JWT SecretKey Length: {secretKey.Length} chars");
Console.WriteLine($"[CONFIG] JWT SecretKey Preview: {keyPreview}...");

// Ensure ConnectionStrings is initialized
if (config.ConnectionStrings == null)
{
    config.ConnectionStrings = new Application.AppConfigurations.ConnectionStrings();
}

// Get connection string from multiple sources (priority: env var > config file)
// Render.com uses environment variables, so check those first
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MSSQLServerDB")
    ?? Environment.GetEnvironmentVariable("MSSQLServerDB")
    ?? builder.Configuration.GetConnectionString("MSSQLServerDB")
    ?? config.ConnectionStrings?.MSSQLServerDB;

// Trim whitespace and validate
if (!string.IsNullOrEmpty(connectionString))
{
    connectionString = connectionString.Trim();
}

if (string.IsNullOrWhiteSpace(connectionString))
{
    // Log all available connection strings for debugging
    Console.WriteLine("[CONFIG] Available connection strings:");
    var allConnectionStrings = builder.Configuration.GetSection("ConnectionStrings").GetChildren();
    foreach (var cs in allConnectionStrings)
    {
        Console.WriteLine($"[CONFIG]   {cs.Key}: {(string.IsNullOrEmpty(cs.Value) ? "(empty)" : cs.Value.Substring(0, Math.Min(50, cs.Value.Length)) + "...")}");
    }
    Console.WriteLine("[CONFIG] Environment variables:");
    var envVar1 = Environment.GetEnvironmentVariable("ConnectionStrings__MSSQLServerDB");
    var envVar2 = Environment.GetEnvironmentVariable("MSSQLServerDB");
    Console.WriteLine($"[CONFIG]   ConnectionStrings__MSSQLServerDB: {(envVar1 != null ? $"exists (length: {envVar1.Length})" : "not found")}");
    Console.WriteLine($"[CONFIG]   MSSQLServerDB: {(envVar2 != null ? $"exists (length: {envVar2.Length})" : "not found")}");
    throw new InvalidOperationException("Connection string 'MSSQLServerDB' is required but not found in configuration or environment variables.");
}

// Validate connection string format (should start with Host= or Server=)
if (!connectionString.Contains("Host=") && !connectionString.Contains("Server="))
{
    Console.WriteLine($"[CONFIG] ERROR: Invalid connection string format. First 100 chars: {connectionString.Substring(0, Math.Min(100, connectionString.Length))}");
    throw new InvalidOperationException("Connection string format is invalid. Expected PostgreSQL format starting with 'Host=' or SQL Server format starting with 'Server='.");
}

config.ConnectionStrings.MSSQLServerDB = connectionString;
var dbHost = connectionString.Split(';').FirstOrDefault(s => s.StartsWith("Host="))?.Replace("Host=", "") 
    ?? connectionString.Split(';').FirstOrDefault(s => s.StartsWith("Server="))?.Replace("Server=", "") 
    ?? "unknown";
Console.WriteLine($"[CONFIG] Database connection configured. Host: {dbHost}");

builder.Services.AddSingleton(config);

// Add dbcontext middlerware
builder.Services.AddDbContextConfiguration(connectionString);

// Add jwt configuration
builder.Services.AddJwtConfiguration(config!);

// Add extensions
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddServiceDIs();

// Add DI for IHttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Add HttpClient for PayOS and other services
builder.Services.AddHttpClient();

// Add auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins(
            "https://artlink-front.vercel.app",  // Domain Vercel của bạn
            "http://localhost:3000"         // Cho dev
        )
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});

// Serilog configuration
//Log.Logger = new LoggerConfiguration()
//      .ReadFrom.Configuration(builder.Configuration).CreateLogger();

//builder.Host.UseSerilog();

// Elasticsearch (disabled if not configured in appsettings)
builder.Services.AddElasticSearch(builder.Configuration);

Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"Credentials/application_default_credentials.json");

// add memory cache
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseSerilogRequestLogging();

// Enable Swagger in all environments (remove for production security)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArtLink API V1");
    c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
});

app.UseHttpsRedirection();

app.UseStaticFiles(); // Use static files

app.UseCors("AllowSpecificOrigin"); // Use CORS with specific policy

// Add logging middleware for debugging
app.Use(async (context, next) =>
{
    var request = context.Request;
    Console.WriteLine($"[REQUEST] {request.Method} {request.Path} | QueryString: {request.QueryString}");
    
    // Log Authorization header if present (skip WebSocket and public endpoints)
    if (!request.Path.Value?.Contains("/ws") ?? false)
    {
        if (request.Headers.ContainsKey("Authorization"))
        {
            var authHeader = request.Headers["Authorization"].ToString();
            Console.WriteLine($"[REQUEST] Authorization: {authHeader.Substring(0, Math.Min(50, authHeader.Length))}...");
        }
        // Removed "No Authorization header" log for public endpoints
    }
    
    await next();
    
    Console.WriteLine($"[RESPONSE] {request.Method} {request.Path} | Status: {context.Response.StatusCode}");
});

app.UseAuthentication(); // Use Authentication

app.UseAuthorization();


app.MapControllers();

// using websocket
app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(10)
});
app.UseApplyMigrations(); // Apply latest migrations, especially when running in Docker

// Elasticsearch initialization disabled (not configured)
//app.UseInitDataElasticSearch(app.Logger); // Init data for elastic search

app.Run();
