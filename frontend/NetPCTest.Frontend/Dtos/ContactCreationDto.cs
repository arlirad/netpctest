namespace NetPCTest.Frontend.Dtos;

// Here we omit the ID, since that is AUTOINCREMENT in SQL. We also omit things specific to relations in EF.
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