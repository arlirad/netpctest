using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Frontend.Dtos;

/// <summary>
/// Represents a Bearer token after a successful login.
/// </summary>
public class LoginTokenDto
{
    [Required]
    public required string Token { get; set; }
}