namespace NetPCTest.Frontend.Dtos;

/// <summary>
/// Represents contact creation data incoming from the frontend.
/// </summary>
public class ContactCreationDto
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public required string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    
    public int CategoryId { get; set; }
    public int? SubCategoryId { get; set; }
    public string? CustomSubCategory { get; set; }
}