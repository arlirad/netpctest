using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Models;

public class Category
{
    public int Id { get; set; }
    [MaxLength(48)]
    public required string Name { get; set; }
    public required bool CustomSubcategoryRequired { get; set; }
    
    public required ICollection<Contact> Members { get; set; }
    public required ICollection<SubCategory> SubCategories { get; set; }
}