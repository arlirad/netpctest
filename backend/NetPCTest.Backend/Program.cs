/*
 * OpenAPI definition: /openapi/v1.json 
 * Swagger: /swagger
 */

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Mappers;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Services;

var builder = WebApplication.CreateBuilder(args);
var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .AddFilter("AutoMapper", LogLevel.Debug)
        .AddConsole();
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5239")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlite(connectionString));
builder.Services.AddRouting(options => 
    options.LowercaseUrls = true);

// Dependency Injection stuff.
builder.Services.AddSingleton<IPasswordHasher<Contact>, PasswordHasher<Contact>>();
builder.Services.AddScoped<IContactsService, ContactsService>();
builder.Services.AddScoped<ILocalisationService, LocalisationService>();
builder.Services.AddSingleton<IMapper>(provider =>
{
    var requiredService = provider.GetRequiredService<ILoggerFactory>();

    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new MappingProfile());
    }, requiredService);
    
    return config.CreateMapper();
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure swagger only when we're in the development environment.
// And allow CORS from our frontend too.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowFrontend");
}

app.MapControllers();
app.UseHttpsRedirection();
app.UseExceptionHandler("/error");

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
    /*if (app.Environment.IsDevelopment())
    {
        DevelopmentBootstrapper.EnsureCategories(db);
        DevelopmentBootstrapper.EnsureSubCategories(db);
    }*/
}

app.Run();