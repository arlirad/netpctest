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
    IPasswordService passwordService,
    IMapper mapper,
    ICategoryValidator validator
) : IContactsService
{
    public Task<int> GetContactCount(CancellationToken cancellationToken) =>
        repository.GetContactCount(cancellationToken);
    
    public async Task<List<ContactBriefDto>> GetContacts(int startIndex, int count, CancellationToken cancellationToken) => 
        mapper.Map<List<ContactBriefDto>>(await repository.GetContacts(startIndex, count, cancellationToken));

    public async Task<ContactDto?> GetContact(int id, CancellationToken cancellationToken)
    {
        var contact = await repository.GetContact(id, cancellationToken);
        
        return contact == null ? null : mapper.Map<ContactDto>(contact);
    }
    
    public async Task<CreateContactResult> CreateContact(ContactCreationDto contactCreationDto)
    {
        var newContact = mapper.Map<Contact>(contactCreationDto);

        var verificationResult = await validator.Validate(newContact);
        
        if (!verificationResult.Success)
            return new CreateContactResult()
            {
                Success = false,
                Message = verificationResult.Message,
            };
        
        newContact.PasswordHash = passwordService.HashPassword(newContact, contactCreationDto.Password);
        
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
}