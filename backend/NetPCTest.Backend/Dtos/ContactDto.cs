namespace NetPCTest.Backend.Dtos;

/// <summary>
/// Represents a contact for use on the frontend.
/// </summary>
public class ContactDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required DateTime BirthDate { get; set; }
    
    public required int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string? CustomSubCategory { get; set; }
}