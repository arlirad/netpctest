using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Frontend.Models;

public class ContactEditFormModel
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Surname { get; set; }
    
    [Required, EmailAddress]
    public string Email { get; set; }
}