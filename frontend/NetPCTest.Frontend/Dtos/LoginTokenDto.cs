using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Frontend.Dtos;

public class LoginTokenDto
{
    [Required]
    public required string Token { get; set; }
}