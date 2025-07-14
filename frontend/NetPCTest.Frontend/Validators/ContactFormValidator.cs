using System.Net.Mail;
using NetPCTest.Frontend.Models;
using NetPCTest.Frontend.Services;

namespace NetPCTest.Frontend.Validators;

public class ContactFormValidator(CategoryService categoryService)
{
    public List<ValidationError> Validate(ContactEditFormModel form, bool checkPassword)
    {
        var errors = new List<ValidationError>();
        
        if (string.IsNullOrEmpty(form.Name))
            errors.Add(new ValidationError("ui.form.contact.name_empty"));
        
        if (string.IsNullOrEmpty(form.Surname))
            errors.Add(new ValidationError("ui.form.contact.surname_empty"));
        
        if (string.IsNullOrEmpty(form.Email))
            errors.Add(new ValidationError("ui.form.contact.email_empty"));
        
        // This is a nice shortcut for writing our own address checker.
        var addr = new MailAddress(form.Email);
        
        if (addr.Address != form.Email)
            errors.Add(new ValidationError("ui.form.contact.email_invalid"));
        
        if (string.IsNullOrEmpty(form.Phone))
            errors.Add(new ValidationError("ui.form.contact.phone_empty"));

        var category = categoryService.GetCategory(form.CategoryId);

        if (category is null)
        {
            errors.Add(new ValidationError("ui.form.contact.category_invalid"));
        }
        else
        {
            var subCategory = categoryService.GetSubCategory(form.SubCategoryId);

            switch (category.CustomSubcategoryRequired)
            {
                case true when string.IsNullOrEmpty(form.CustomSubCategory):
                    errors.Add(new ValidationError("ui.form.contact.custom_subcategory_empty"));
                    break;
                case false when (subCategory is null && 
                                category.SubCategories.Count >= 0) ||
                                (subCategory is not null &&
                                category.SubCategories.Count > 0 &&
                                category.SubCategories.All(s => s.Id != subCategory.Id)):
                    errors.Add(new ValidationError("ui.form.contact.subcategory_invalid"));
                    break;
            }
        }

        if (!checkPassword)
            return errors;

        if (string.IsNullOrEmpty(form.Password))
        {
            errors.Add(new ValidationError("ui.form.contact.password_empty"));
        }
        else
        {
            if (form.Password.Length < 8 ||
                !form.Password.Any(char.IsUpper) ||
                !form.Password.Any(char.IsLower) ||
                !form.Password.Any(char.IsDigit) || 
                form.Password.All(char.IsLetterOrDigit))
                errors.Add(new ValidationError("ui.form.contact.password_insecure"));
        }
        
        if (form.Password != form.ConfirmPassword)
            errors.Add(new ValidationError("ui.form.contact.password_no_match"));

        return errors;
    }
}