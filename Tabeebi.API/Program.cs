var builder = WebApplication.CreateBuilder(args);


// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "Tabeebi Clinic Management API",
        Version = "v1",
        Description = "Clean Architecture Test"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tabeebi API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Simple test endpoint
app.MapGet("/api/test", () => new
{
    Message = "Tabeebi Clinic Management API - Clean Architecture Test",
    Version = "1.0.0",
    Architecture = "Clean Architecture",
    Layers = new[]
    {
        "Core (Entities)",
        "Domain (Business Logic)",
        "Infrastructure (Data Access)",
        "API (Controllers & Endpoints)"
    },
    Timestamp = DateTime.UtcNow
})
.WithName("TestEndpoint")
.WithOpenApi();

app.Run();
