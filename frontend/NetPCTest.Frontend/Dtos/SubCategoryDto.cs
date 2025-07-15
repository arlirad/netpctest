namespace NetPCTest.Frontend.Dtos;

/// <summary>
/// Represents a subcategory for use on the frontend.
/// </summary>
public class SubCategoryDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public int CategoryId { get; set; }
}