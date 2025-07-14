using System.Collections;
using System.Net.Http.Json;
using NetPCTest.Frontend.Dtos;

namespace NetPCTest.Frontend.Services;

public class CategoryService(HttpClient httpClient)
{
    private List<CategoryDto> _categories = [];
    private List<SubCategoryDto> _subCategories = [];
    
    public async Task RefreshCategories()
    {
        var categories = 
            await httpClient.GetFromJsonAsync<List<CategoryDto>>("categories");

        if (categories != null)
        {
            _categories = categories;
            _subCategories = [];
            
            _categories.ForEach(c => _subCategories.AddRange(c.SubCategories));
        }
    }

    public CategoryDto? GetCategory(int id) => _categories.FirstOrDefault(c => c.Id == id);

    public SubCategoryDto? GetSubCategory(int id)  => _subCategories.FirstOrDefault(s => s.Id == id);

    public IReadOnlyList<CategoryDto> GetCategories() => _categories;
}