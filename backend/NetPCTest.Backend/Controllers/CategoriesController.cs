using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

/// <summary>
/// API controller responsible for handling operations related to categories and subcategories.
/// Applies more relaxed rate limiting, as categories are less resource-intensive.
/// </summary>
/// <param name="categoriesService">The service handling category operations.</param>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoriesService categoriesService) : ControllerBase
{
    /// <summary>
    /// Returns the count of <see cref="Category"/> entites in the form of <see cref="CategoryDto"/>.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to observe while handling the request.</param>
    /// <returns>Category count.</returns>
    [HttpGet("count")]
    public async Task<IActionResult> GetCategoryCount(CancellationToken cancellationToken)
    {
        var count = await categoriesService.GetCategoryCountAsync(cancellationToken);
        
        return Ok(new { Count = count });
    }
    
    /// <summary>
    /// Returns a list of <see cref="CategoryDto"/> containing <see cref="SubCategoryDto"/> as children.
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/> to observe while handling the request.</param>
    /// <returns>List of <see cref="CategoryDto"/>.</returns>
    [EnableRateLimiting("categories")]
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var contacts = await categoriesService.GetCategoriesAsync(cancellationToken);
        
        return Ok(contacts);
    }
}