using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Models;

/// <summary>
/// Represents a LocaleKeyString entity for use in localisation.
/// </summary>
public class LocaleKeyString
{
    [MaxLength(64)]
    public required string Key { get; set; }
    [MaxLength(1024)]
    public required string Value { get; set; }
    
    public required int LocaleId { get; set; }
    public required Locale Locale { get; set; }
}