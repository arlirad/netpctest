using NetPCTest.Backend.Data;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend;

// A class to simplify testing.
public static class DevelopmentBootstrapper
{
    public static void EnsureCategories(AppDbContext db)
    {
        if (db.Categories.Select(c => c.Name).Any(n => n == "#category.other"))
            return;
        
        db.Categories.Add(new Category
        {
            Name = "#category.other",
            CustomSubcategoryAllowed = true,
        });
        db.Categories.Add(new Category
        {
            Name = "#category.work",
            CustomSubcategoryAllowed = false,
        });
        db.Categories.Add(new Category
        {
            Name = "#category.personal",
            CustomSubcategoryAllowed = false,
        });
        
        db.SaveChanges();
    }
    
    public static void EnsureSubCategories(AppDbContext db)
    {
        if (db.SubCategories.Select(s => s.Name).Any(n => n == "#subcategory.manager"))
            return;
        
        db.SubCategories.Add(new SubCategory
        {
            Name = "#subcategory.manager",
            Category = db.Categories.Single(c => c.Name == "#category.work"),
        });
        db.SubCategories.Add(new SubCategory
        {
            Name = "#subcategory.employee",
            Category = db.Categories.Single(c => c.Name == "#category.work"),
        });
        db.SubCategories.Add(new SubCategory
        {
            Name = "#subcategory.consultant",
            Category = db.Categories.Single(c => c.Name == "#category.work"),
        });
        db.SubCategories.Add(new SubCategory
        {
            Name = "#subcategory.client",
            Category = db.Categories.Single(c => c.Name == "#category.work"),
        });
        
        db.SaveChanges();
    }
}