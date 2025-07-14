using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Dtos;

public class LoginTokenDto
{
    [Required]
    public required string Token { get; set; }
}