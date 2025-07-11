using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Dtos;

// Here we omit the password hash and things specific to relations in EF.
public class ContactDetailsDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required DateTime BirthDate { get; set; }
    
    public required int CategoryId { get; set; }
    public required int SubCategoryId { get; set; }
    public string? CustomSubCategory { get; set; }
}