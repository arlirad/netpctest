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

            foreach (var category in _categories)
            {
                foreach (var subCategory in category.SubCategories)
                {
                    if (_subCategories.FirstOrDefault(s => s.Id == subCategory.Id) is not null)
                        continue;
                    
                    _subCategories.Add(subCategory);
                }
            }
        }
    }

    public CategoryDto? GetCategory(int id) => _categories.FirstOrDefault(c => c.Id == id);

    public SubCategoryDto? GetSubCategory(int id)  => _subCategories.FirstOrDefault(s => s.Id == id);
}