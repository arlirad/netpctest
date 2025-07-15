namespace NetPCTest.Frontend.Dtos;

/// <summary>
/// Represents a category for use on the frontend.
/// </summary>
public class CategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required bool CustomSubcategoryRequired { get; set; }

    public List<SubCategoryDto> SubCategories { get; set; } = [];
}