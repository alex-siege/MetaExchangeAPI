var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adds controller services to the application.
builder.Services.AddControllers();

// Adds support for API documentation using Swagger/OpenAPI.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// Enable Swagger UI and middleware only in development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Add authorization middleware to the request pipeline.
app.UseAuthorization();

// Map controller endpoints to handle incoming HTTP requests.
app.MapControllers();

// Run the application.
app.Run();
