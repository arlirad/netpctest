using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Models;

public class LocaleKeyString
{
    [MaxLength(64)]
    public required string Key { get; set; }
    [MaxLength(1024)]
    public required string Value { get; set; }
    
    public int LocaleId { get; set; }
    public Locale Locale { get; set; }
}