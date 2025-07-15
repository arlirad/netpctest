namespace NetPCTest.Backend.Results;

/// <summary>
/// Represents a result of category validation including a specific error message.
/// </summary>
public class CategoryValidationResult
{
    public bool Success { get; set; }
    public string? Message { get; set; } = null;
}