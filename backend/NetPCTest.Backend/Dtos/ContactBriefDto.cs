using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Dtos;

// A DTO specifically for listing all contacts in a list.
public class ContactBriefDto
{
    public int Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Surname { get; set; }

    public static ContactBriefDto FromIdNameSurname(int id, string name, string surname) 
        => new() 
        {
            Id = id,
            Name = name,
            Surname = surname
        };
}