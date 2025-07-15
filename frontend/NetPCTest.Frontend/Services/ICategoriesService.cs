using NetPCTest.Frontend.Dtos;

namespace NetPCTest.Frontend.Services;

/// <summary>
/// Defines an abstraction for accessing category data.
/// </summary>
public interface ICategoriesService
{
    /// <summary>
    /// Refreshes the list of categories from the data source.
    /// </summary>
    /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
    Task RefreshCategoriesAsync();
    
    /// <summary>
    /// Retrieves a category with the specified ID from the local in-memory cache.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>The matching <see cref="CategoryDto"/>, or null if not found.</returns>
    CategoryDto? GetCategory(int id);
    
    /// <summary>
    /// Retrieves a subcategory with the specified ID from the local in-memory cache.
    /// </summary>
    /// <param name="id">The ID of the subcategory to retrieve.</param>
    /// <returns>The matching <see cref="SubCategoryDto"/>, or null if not found.</returns>
    SubCategoryDto? GetSubCategory(int id);
    
    /// <summary>
    /// Retrieves a list of categories from the local in-memory cache.
    /// </summary>
    /// <returns>A read only list containing <see cref="CategoryDto"/>.</returns>
    IReadOnlyList<CategoryDto> GetCategories();
}