using System.ComponentModel.DataAnnotations;
using NetPCTest.Backend.Validators;

namespace NetPCTest.Backend.Dtos;

/// <summary>
/// Represents contact creation data incoming from the frontend.
/// </summary>
public partial class ContactCreationDto
{
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Surname { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [StrongPassword]
    public required string Password { get; set; }
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
    [Required]
    [Phone]
    public required string Phone { get; set; }
    [Required]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "01.01.1900", "01.01.9999")]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string? CustomSubCategory { get; set; }
}