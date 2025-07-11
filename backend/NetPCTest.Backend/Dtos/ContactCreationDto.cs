using System.ComponentModel.DataAnnotations;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Dtos;

// Here we omit the ID, since that is AUTOINCREMENT in SQL. We also omit things specific to relations in EF.
public class ContactCreationDto
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Surname { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
    [Required]
    public required string Phone { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string? CustomSubCategory { get; set; }
}