using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Models;

public class Contact
{
    public int Id { get; set; }
    [MaxLength(32)]
    public string Name { get; set; }
    [MaxLength(48)]
    public string Surname { get; set; }
    // The RFCs define max mailbox length at 256, however two characters are taken
    // up by front and trailing < and >. That way, our limit can safely be at 254
    // characters.
    [MaxLength(254)]
    public string Email { get; set; }
    // Max length: https://joshthecoder.com/2020/04/28/max-data-types-in-aspnet-core-identity-schema.html
    // However, we will round it up to 96.
    [MaxLength(96)]
    public string PasswordHash { get; set; }
    // Max length: https://en.wikipedia.org/wiki/E.164, but we will round it up to 16.
    [MaxLength(16)]
    public string Phone { get; set; }
    public DateTime BirthDate { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int SubCategoryId { get; set; }
    public SubCategory SubCategory { get; set; }
    [MaxLength(48)]
    public string? CustomSubCategory { get; set; }
}