using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using NetPCTest.Backend.Services;

namespace NetPCTest.Backend.Controllers;

/*
 * Controller responsible for categories.
 * With more relaxed rate limiting due to categories using up less memory/bandwidth/processing-intensive.
 */
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoriesService categoriesService) : ControllerBase
{
    [HttpGet("count")]
    public async Task<IActionResult> GetCategoryCount(CancellationToken cancellationToken)
    {
        var count = await categoriesService.GetCategoryCount(cancellationToken);
        
        return Ok(new { Count = count });
    }
    
    [EnableRateLimiting("categories")]
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var contacts = await categoriesService.GetCategories(cancellationToken);
        
        return Ok(contacts);
    }
}