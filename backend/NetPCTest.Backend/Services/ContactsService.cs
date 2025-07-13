using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Services;

public class ContactsService(AppDbContext context, IPasswordHasher<Contact> passwordHasher) : IContactsService
{
    public async Task<List<ContactBriefDto>> GetContacts(int startIndex, int count)
    {
        // Here we first .Select to make sure EF doesn't create a SQL query with columns that we are not going to use
        // anyway. Then, we .Select to create the DTOs.
        var contacts = await context.Contacts
            .Select(c => new { c.Id, c.Name, c.Surname })
            .OrderBy(c => c.Id)
            .Skip(startIndex)
            .Take(count)
            .Select(c => ContactBriefDto.FromIdNameSurname(c.Id, c.Name, c.Surname))
            .ToListAsync();
        
        return contacts;
    }

    public async Task<ContactDetailsDto?> GetContactDetails(int id)
    {
        var contact = await context.Contacts
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        
        if (contact == null)
            return null;

        return new ContactDetailsDto
        {
            Id = contact.Id,
            Name = contact.Name,
            Surname = contact.Surname,
            Email = contact.Email,
            Phone = contact.Phone,
            BirthDate = contact.BirthDate,
            CategoryId = contact.CategoryId,
            SubCategoryId = contact.SubCategoryId,
        };
    }
    
    public async Task<CreateContactResult> CreateContact(ContactCreationDto contactCreationDto)
    {
        var newContact = new Contact
        {
            Name = contactCreationDto.Name,
            Surname = contactCreationDto.Surname,
            Email = contactCreationDto.Email,
            Phone = contactCreationDto.Phone,
            BirthDate = contactCreationDto.BirthDate,
            CategoryId = contactCreationDto.CategoryId,
            SubCategoryId = contactCreationDto.SubCategoryId,
            CustomSubCategory = contactCreationDto.CustomSubCategory,
        };

        var verificationResult = await VerifyCategoryAndSubCategory(newContact, contactCreationDto);
        
        if (verificationResult is not null)
            return verificationResult;
        
        newContact.PasswordHash = HashPassword(newContact, contactCreationDto.Password);
        
        var addResult = await context.Contacts.AddAsync(newContact);
        await context.SaveChangesAsync();
        
        return new CreateContactResult
        {
            Success = true, 
            Message = "contacts.creation.success",
            Id = addResult.Entity.Id,
        };
    }

    public string HashPassword(Contact contact, string password)
    {
        return passwordHasher.HashPassword(contact, password);
    }

    public bool ComparePassword(Contact contact, string hashedPassword, string providedPlainPassword)
    {
        var result = passwordHasher.VerifyHashedPassword(contact, hashedPassword, providedPlainPassword);
        
        return result != PasswordVerificationResult.Failed;
    }

    private async Task<CreateContactResult?> VerifyCategoryAndSubCategory(Contact newContact, ContactCreationDto contactCreationDto)
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