using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Models;

/// <summary>
/// Represents a subcategory entity.
/// </summary>
public class SubCategory
{
    public int Id { get; set; }
    [MaxLength(48)]
    public required string Name { get; set; }
    
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
    
    public required ICollection<Contact> Members { get; set; }
}