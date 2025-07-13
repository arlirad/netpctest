using NetPCTest.Backend.Dtos;

namespace NetPCTest.Backend.Services;

public interface ICategoriesService
{
    Task<int> GetCategoryCount(CancellationToken cancellationToken);
    Task<List<CategoryDto>> GetCategories(CancellationToken cancellationToken);
}