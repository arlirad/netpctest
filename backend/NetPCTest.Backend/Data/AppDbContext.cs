using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Category 1:N Contact (represented as Members inside Category).
        builder.Entity<Contact>()
            .HasOne(c => c.Category)
            .WithMany(c => c.Members)
            .HasForeignKey(c => c.CategoryId)
            .HasPrincipalKey(c => c.Id);

        // SubCategory 1:N Contact (represented as Members inside SubCategory).
        builder.Entity<Contact>()
            .HasOne(c => c.SubCategory)
            .WithMany(s => s.Members)
            .HasForeignKey(c => c.SubCategoryId)
            .HasPrincipalKey(s => s.Id);
        
        // Ensure that Contacts have unique emails.
        builder.Entity<Contact>()
            .HasIndex(c => c.Email)
            .IsUnique();
        
        // Category 1:N SubCategory (represented as SubCategories inside Category).
        builder.Entity<SubCategory>()
            .HasOne(s => s.Category)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.CategoryId)
            .HasPrincipalKey(c => c.Id);
        
        base.OnModelCreating(builder);
    }
}