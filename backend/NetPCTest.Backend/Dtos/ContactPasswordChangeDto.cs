using System.ComponentModel.DataAnnotations;
using NetPCTest.Backend.Validators;

namespace NetPCTest.Backend.Dtos;

public class ContactPasswordChangeDto
{
    [Required]
    [StrongPassword]
    public required string Password { get; set; }
    [Compare("Password")]
    public required string ConfirmPassword { get; set; }
}