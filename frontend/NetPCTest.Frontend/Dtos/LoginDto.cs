using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Frontend.Dtos;

/// <summary>
/// Represents login credentials from the frontend.
/// </summary>
public class LoginDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Password { get; set; }
}