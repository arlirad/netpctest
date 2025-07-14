using AutoMapper;
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

    public async Task<UpdateContactResult> UpdateContact(int id, ContactUpdateDto newData)
    {
        var contact = await repository.GetContact(id, CancellationToken.None);
        if (contact is null)
            return UpdateContactResult.NotFound;

        if (newData.Email != contact.Email && 
            await repository.GetContactByEmail(newData.Email, CancellationToken.None) is not null)
            return UpdateContactResult.Invalid;
        
        mapper.Map(newData, contact);

        var validationResult = await validator.Validate(contact);
        
        if (!validationResult.Success)
            return UpdateContactResult.Invalid;
        
        return await repository.UpdateContact(id, contact) ? 
            UpdateContactResult.Success : 
            UpdateContactResult.Invalid;
    }

    public async Task<bool> SetContactPassword(int id, ContactPasswordChangeDto newData)
    {
        if (newData.Password != newData.ConfirmPassword)
            return false;
        
        var contact = await repository.GetContact(id, CancellationToken.None);
        if (contact is null)
            return false;
        
        contact.PasswordHash = passwordService.HashPassword(contact, newData.Password);

        return await repository.UpdateContact(id, contact);
    }

    public async Task<bool> DeleteContact(int id) => 
        await repository.DeleteContact(id);

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