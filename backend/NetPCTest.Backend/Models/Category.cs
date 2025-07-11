using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetPCTest.Backend.Models;

public class Category
{
    public int Id { get; set; }
    [MaxLength(48)]
    public required string Name { get; set; }
    public required bool CustomSubcategoryAllowed { get; set; }
    
    public ICollection<Contact> Members { get; set; }
    public ICollection<SubCategory> SubCategories { get; set; }
}