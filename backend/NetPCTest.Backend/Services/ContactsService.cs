using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Repositories;
using NetPCTest.Backend.Results;
using NetPCTest.Backend.Validators;

namespace NetPCTest.Backend.Services;

public class ContactsService(
    IRepository repository, 
    IPasswordHasher<Contact> passwordHasher, 
    IMapper mapper,
    ICategoryValidator validator
) : IContactsService
{
    public async Task<List<ContactBriefDto>> GetContacts(int startIndex, int count) 
        => mapper.Map<List<ContactBriefDto>>(await repository.GetContacts(startIndex, count));

    public async Task<ContactDto?> GetContact(int id)
    {
        var contact = await repository.GetContact(id);
        
        return contact == null ? null : mapper.Map<ContactDto>(contact);
    }
    
    public async Task<CreateContactResult> CreateContact(ContactCreationDto contactCreationDto)
    {
        var newContact = mapper.Map<Contact>(contactCreationDto);

        var verificationResult = await validator.Validate(newContact, contactCreationDto);
        
        if (verificationResult is not null)
            return verificationResult;
        
        newContact.PasswordHash = HashPassword(newContact, contactCreationDto.Password);
        
        var id = await repository.CreateContact(newContact);
        
        if (id is null)
            return new CreateContactResult
            {
                Success = false, 
                Message = "contacts.creation.failure"
            };
        
        return new CreateContactResult
        {
            Success = true, 
            Message = "contacts.creation.success",
            Id = id.Value,
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