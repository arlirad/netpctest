using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Services;

/// <summary>
/// Provides an abstraction for accessing <see cref="Category"/> entities.
/// </summary>
public interface ICategoriesService
{
    /// <summary>
    /// Returns the count of categories asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation. The result of the task is the count of categories.</returns>
    Task<int> GetCategoryCountAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Returns all categories asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation. The result of the task is a list of categories.</returns>
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken);
}