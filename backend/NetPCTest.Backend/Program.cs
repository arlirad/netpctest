/*
 * OpenAPI definition: /openapi/v1.json 
 * Swagger: /swagger
 */

using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(connectionString));
builder.Services.AddRouting(options => 
    options.LowercaseUrls = true);
builder.Services.AddScoped<IContactsService, ContactsService>();
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

    // Yeah, this seems a little janky, but at least it will let the people who are going to review this test it
    // properly.
    if (app.Environment.IsDevelopment())
    {
        DevelopmentBootstrapper.EnsureCategories(db);
        DevelopmentBootstrapper.EnsureSubCategories(db);
    }
}

app.Run();
