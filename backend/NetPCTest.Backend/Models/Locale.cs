using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Models;

/// <summary>
/// Represents a locale entity.
/// </summary>
public class Locale
{
    public int Id { get; set; }
    
    [MaxLength(8)]
    public required string Name { get; set; }
    
    public required ICollection<LocaleKeyString> KeyStrings { get; set; }
}