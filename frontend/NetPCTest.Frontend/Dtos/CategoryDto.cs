using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NetPCTest.Frontend.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required bool CustomSubcategoryRequired { get; set; }
    
    public List<SubCategoryDto> SubCategories { get; set; }
}