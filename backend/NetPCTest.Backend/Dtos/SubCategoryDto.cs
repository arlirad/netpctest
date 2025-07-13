using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetPCTest.Backend.Dtos;

public class SubCategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public int CategoryId { get; set; }
}