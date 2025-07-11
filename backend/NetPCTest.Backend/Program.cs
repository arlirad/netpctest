/*
 * OpenAPI definition: /openapi/v1.json 
 * Swagger: /swagger
 */

using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(connectionString));
builder.Services.AddRouting(options => 
    options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure swagger only when we're in the development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

// Map Swagger only when we're in the development environment.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapSwagger();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
