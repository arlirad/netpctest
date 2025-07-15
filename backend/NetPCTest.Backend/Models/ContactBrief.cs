namespace NetPCTest.Backend.Models;

/// <summary>
/// Represents a contact entity in a condensed way, as to be suitable for listing.
/// </summary>
public class ContactBrief
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
}