using AutoMapper;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Repositories;

namespace NetPCTest.Backend.Services;

/// <summary>
/// Implements logic used to access <see cref="Category"/> and <see cref="SubCategory"/> entities.
/// </summary>
/// <param name="repository">The repository used to access category data.</param>
/// <param name="mapper">The AutoMapper instance for mapping between domain models and DTOs.</param>
public class CategoriesService(IRepository repository, IMapper mapper) : ICategoriesService
{
    public async Task<int> GetCategoryCountAsync(CancellationToken cancellationToken) =>
        await repository.GetCategoryCountAsync(cancellationToken);

    public async Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken cancellationToken) =>
        mapper.Map<List<CategoryDto>>(await repository.GetCategoriesAsync(cancellationToken));
}