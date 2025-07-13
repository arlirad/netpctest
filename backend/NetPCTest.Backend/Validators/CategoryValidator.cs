using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Validators;

public class CategoryValidator(AppDbContext context) : ICategoryValidator
{
    public async Task<CreateContactResult?> Validate(Contact newContact, ContactCreationDto contactCreationDto)
    {
        var category = await context.Categories
            .Include(c => c.SubCategories)
            .FirstOrDefaultAsync(c => c.Id == newContact.CategoryId);

        if (category is null)
            return new CreateContactResult
            {
                Success = false,
                Message = "contacts.creation.category.doesnt_exist",
            };

        if (category.CustomSubcategoryRequired && newContact.CustomSubCategory is null)
            return new CreateContactResult
            {
                Success = false,
                Message = "contacts.creation.category.custom_subcategory_required",
            };

        if (!category.CustomSubcategoryRequired && !newContact.SubCategoryId.HasValue)
            return new CreateContactResult
            {
                Success = false,
                Message = "contacts.creation.category.fixed_subcategory_required",
            };

        if (newContact.SubCategoryId.HasValue && 
            !await context.SubCategories.AnyAsync(c => c.Id == newContact.SubCategoryId))
            return new CreateContactResult
            {
                Success = false,
                Message = "contacts.creation.subcategory.doesnt_exist",
            };

        if (newContact.SubCategoryId.HasValue && 
            category.SubCategories.All(c => c.Id != newContact.SubCategoryId.Value))
            return new CreateContactResult
            {
                Success = false,
                Message = "contacts.creation.subcategory.doesnt_belong_to_given_category",
            };

        return null;
    }
}