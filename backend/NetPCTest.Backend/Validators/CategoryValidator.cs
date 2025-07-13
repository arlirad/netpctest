using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Repositories;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Validators;

public class CategoryValidator(IRepository repository) : ICategoryValidator
{
    public async Task<CategoryValidationResult> Validate(Contact newContact, CancellationToken cancellationToken)
    {
        var category = await repository.GetCategory(newContact.CategoryId, cancellationToken);

        if (category is null)
            return new CategoryValidationResult
            {
                Success = false,
                Message = "contacts.creation.category.doesnt_exist",
            };

        if (category.CustomSubcategoryRequired && newContact.CustomSubCategory is null)
            return new CategoryValidationResult
            {
                Success = false,
                Message = "contacts.creation.category.custom_subcategory_required",
            };

        if (!category.CustomSubcategoryRequired && !newContact.SubCategoryId.HasValue)
            return new CategoryValidationResult
            {
                Success = false,
                Message = "contacts.creation.category.fixed_subcategory_required",
            };

        if (newContact.SubCategoryId.HasValue && 
            await repository.GetSubCategory(newContact.SubCategoryId.Value, cancellationToken) == null)
            return new CategoryValidationResult
            {
                Success = false,
                Message = "contacts.creation.subcategory.doesnt_exist",
            };

        if (newContact.SubCategoryId.HasValue && 
            category.SubCategories.All(c => c.Id != newContact.SubCategoryId.Value))
            return new CategoryValidationResult
            {
                Success = false,
                Message = "contacts.creation.subcategory.doesnt_belong_to_given_category",
            };

        return new CategoryValidationResult
        {
            Success = true,
        };
    }
}