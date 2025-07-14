using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Dtos;

public class ContactPasswordChangeDto
{
    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}