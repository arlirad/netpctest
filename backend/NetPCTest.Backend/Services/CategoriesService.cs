using AutoMapper;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Repositories;

namespace NetPCTest.Backend.Services;

public class CategoriesService(IRepository repository, IMapper mapper) : ICategoriesService
{
    public async Task<int> GetCategoryCount(CancellationToken cancellationToken) =>
        await repository.GetCategoryCount(cancellationToken);

    public async Task<List<CategoryDto>> GetCategories(CancellationToken cancellationToken) =>
        mapper.Map<List<CategoryDto>>(await repository.GetCategories(cancellationToken));
}