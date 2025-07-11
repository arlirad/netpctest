using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Models;

public class Category
{
    public int Id { get; set; }
    [MaxLength(48)]
    public string Name { get; set; }
    public ICollection<Contact> Members { get; set; }
    public ICollection<SubCategory> SubCategories { get; set; }
}