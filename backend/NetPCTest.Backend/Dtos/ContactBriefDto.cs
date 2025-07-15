using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Dtos;

/// <summary>
/// Represents a contact in a way that is suitable for display in a list.
/// <remarks>Who needs every single property in a list, right?</remarks>
/// </summary>
public class ContactBriefDto
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Surname { get; set; }
}