// Create the WebApplicationBuilder to configure services and the HTTP pipeline.
var builder = WebApplication.CreateBuilder(args);

//Connecting database
var rawConnection = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString =
    rawConnection
        .Replace("${DB_HOST}", Environment.GetEnvironmentVariable("DB_HOST"))
        .Replace("${DB_NAME}", Environment.GetEnvironmentVariable("DB_NAME"))
        .Replace("${DB_USER}", Environment.GetEnvironmentVariable("DB_USER"))
        .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

builder.Services.AddSingleton(new PersonRepository(connectionString));

// Register MVC controller services.
builder.Services.AddControllers();

// Build the WebApplication instance.
var app = builder.Build();

// Map controller routes to endpoints.
app.MapControllers();

// Start the application and listen for HTTP requests.
app.Run();