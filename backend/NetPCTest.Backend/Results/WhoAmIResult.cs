namespace NetPCTest.Backend.Results;

/// <summary>
/// Represents the currently logged-in user.
/// </summary>
public class WhoAmIResult
{
    public required int Id { get; set; }
    public required string Email { get; set; }
}