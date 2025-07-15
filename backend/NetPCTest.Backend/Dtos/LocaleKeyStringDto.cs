using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Dtos;

/// <summary>
/// Represents a <see cref="LocaleKeyString"/> for use on the frontend.
/// </summary>
public class LocaleKeyStringDto
{
    public required string Key { get; set; }
    public required string Value { get; set; }
}