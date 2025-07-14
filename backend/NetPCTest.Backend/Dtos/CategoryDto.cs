namespace NetPCTest.Backend.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required bool CustomSubcategoryRequired { get; set; }
    
    public required List<SubCategoryDto> SubCategories { get; set; }
}