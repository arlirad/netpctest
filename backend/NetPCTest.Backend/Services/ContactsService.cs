using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;
using NetPCTest.Backend.Validators;

namespace NetPCTest.Backend.Services;

public class ContactsService(
    AppDbContext context, 
    IPasswordHasher<Contact> passwordHasher, 
    IMapper mapper,
    ICategoryValidator validator
) : IContactsService
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
        
        return contact == null ? null : mapper.Map<ContactDetailsDto>(contact);
    }
    
    public async Task<CreateContactResult> CreateContact(ContactCreationDto contactCreationDto)
    {
        var newContact = mapper.Map<Contact>(contactCreationDto);

        var verificationResult = await validator.Validate(newContact, contactCreationDto);
        
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
}