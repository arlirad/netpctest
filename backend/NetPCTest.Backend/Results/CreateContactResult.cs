namespace NetPCTest.Backend.Results;

/// <summary>
/// Represents the result of a contact creation. Indicates success, a description message if applicable and an ID
/// of the created contact.
/// </summary>
public class CreateContactResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public int? Id { get; set; }
}