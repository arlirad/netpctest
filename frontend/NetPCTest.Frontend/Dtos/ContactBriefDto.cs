namespace NetPCTest.Frontend.Dtos;

// A DTO specifically for listing contacts in a list.
public class ContactBriefDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
}