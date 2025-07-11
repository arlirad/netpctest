using Microsoft.EntityFrameworkCore;

namespace NetPCTest.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    
}