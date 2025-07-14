using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Frontend.Dtos;

public class ContactPasswordChangeDto
{
    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}