using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace NetPCTest.Backend.Dtos;

public partial class ContactCreationDto : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();
        
        if (SubCategoryId.HasValue && !string.IsNullOrWhiteSpace(CustomSubCategory))
            results.Add(new ValidationResult("ui.error.subcategory_or_custom_subcategory_both_not_permitted", 
            [
                nameof(SubCategoryId), nameof(CustomSubCategory)
            ]));
        
        return results;
    }
}