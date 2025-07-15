using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Frontend.Dtos;

/// <summary>
/// Separates password setting from other contact edition.
/// </summary>
public class ContactPasswordChangeDto
{
    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}