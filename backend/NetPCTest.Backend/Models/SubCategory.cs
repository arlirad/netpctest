using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetPCTest.Backend.Models;

public class SubCategory
{
    public int Id { get; set; }
    [MaxLength(48)]
    public required string Name { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public ICollection<Contact> Members { get; set; }
}