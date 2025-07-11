using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Models;

public class Locale
{
    public int Id { get; set; }
    
    [MaxLength(8)]
    public string Name { get; set; }
    
    public ICollection<LocaleKeyString> KeyStrings { get; set; }
}