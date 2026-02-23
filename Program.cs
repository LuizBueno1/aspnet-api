// Create the WebApplicationBuilder to configure services and the HTTP pipeline.
var builder = WebApplication.CreateBuilder(args);

// Register MVC controller services.
builder.Services.AddControllers();

// Build the WebApplication instance.
var app = builder.Build();

// Map controller routes to endpoints.
app.MapControllers();

// Start the application and listen for HTTP requests.
app.Run();