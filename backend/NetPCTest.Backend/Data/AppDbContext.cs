using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }
    public DbSet<Locale> Locales { get; set; }
    public DbSet<LocaleKeyString> LocaleKeyStrings { get; set; }
    
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
        
        // Locale 1:N LocaleKeyString (represented as KeyStrings inside Locale).
        builder.Entity<LocaleKeyString>()
            .HasOne(l => l.Locale)
            .WithMany(l => l.KeyStrings)
            .HasForeignKey(l => l.LocaleId)
            .HasPrincipalKey(l => l.Id);
        
        // Ensure that Locales have unique names.
        builder.Entity<Locale>()
            .HasIndex(l => l.Name)
            .IsUnique();
        
        // LocaleKeyString needs a key.
        builder.Entity<LocaleKeyString>()
            .HasKey(l => new { l.Key, l.LocaleId });
        
        base.OnModelCreating(builder);
    }
}