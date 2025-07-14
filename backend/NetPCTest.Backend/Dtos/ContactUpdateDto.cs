using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Dtos;

// We omit the ID, since that's already specified by the PUT request itself.
// Passwords are kinda their own thing.
public class ContactUpdateDto
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Surname { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [Phone]
    public required string Phone { get; set; }
    [Required]
    [Range(typeof(DateTime), "01.01.1900", "01.01.9999")]
    public required DateTime BirthDate { get; set; }
    
    [Required]
    public required int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string? CustomSubCategory { get; set; }
}