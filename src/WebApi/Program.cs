using Application;
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
builder.Configuration.Bind(config);

// 🔍 DEBUG: Log SecretKey length to verify env vars are loaded (NOT the actual key!)
Console.WriteLine($"[CONFIG] JWT Issuer: {config?.JwtConfiguration?.Issuer}");
Console.WriteLine($"[CONFIG] JWT SecretKey Length: {config?.JwtConfiguration?.SecretKey?.Length ?? 0} chars");
Console.WriteLine($"[CONFIG] JWT SecretKey First 10 chars: {config?.JwtConfiguration?.SecretKey?.Substring(0, Math.Min(10, config.JwtConfiguration.SecretKey?.Length ?? 0))}...");

builder.Services.AddSingleton(config!);

// Add dbcontext middlerware
builder.Services.AddDbContextConfiguration(config!.ConnectionStrings.MSSQLServerDB);

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
