using AutoMapper;
using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Repositories;
using NetPCTest.Backend.Results;
using NetPCTest.Backend.Validators;

namespace NetPCTest.Backend.Services;

/// <summary>
/// Implementation of the <see cref="IContactsService"/> interface utilizing <see cref="IRepository"/>.
/// </summary>
/// <param name="repository"><see cref="IRepository"/> used for access to data.</param>
/// <param name="passwordService"><see cref="IPasswordService"/> providing password operations.</param>
/// <param name="mapper">AutoMapper.</param>
/// <param name="validator"><see cref="ICategoryValidator"/> used for verifying categories on contact creation and editing.</param>
public class ContactsService(
    IRepository repository, 
    IPasswordService passwordService,
    IMapper mapper,
    ICategoryValidator validator
) : IContactsService
{
    public Task<int> GetContactCountAsync(CancellationToken cancellationToken) =>
        repository.GetContactCountAsync(cancellationToken);
    
    public async Task<List<ContactBriefDto>> GetContactsAsync(int startIndex, int count, CancellationToken cancellationToken) => 
        mapper.Map<List<ContactBriefDto>>(await repository.GetContactsAsync(startIndex, count, cancellationToken));

    public async Task<ContactDto?> GetContactAsync(int id, CancellationToken cancellationToken)
    {
        var contact = await repository.GetContactAsync(id, cancellationToken);
        
        return contact == null ? null : mapper.Map<ContactDto>(contact);
    }

    public async Task<UpdateContactResult> UpdateContactAsync(int id, ContactUpdateDto newData)
    {
        var contact = await repository.GetContactAsync(id, CancellationToken.None);
        if (contact is null)
            return UpdateContactResult.NotFound;

        if (newData.Email != contact.Email && 
            await repository.GetContactByEmailAsync(newData.Email, CancellationToken.None) is not null)
            return UpdateContactResult.Invalid;
        
        mapper.Map(newData, contact);

        var validationResult = await validator.Validate(contact);
        
        if (!validationResult.Success)
            return UpdateContactResult.Invalid;
        
        return await repository.UpdateContactAsync(id, contact) ? 
            UpdateContactResult.Success : 
            UpdateContactResult.Invalid;
    }

    public async Task<bool> SetContactPasswordAsync(int id, ContactPasswordChangeDto newData)
    {
        if (newData.Password != newData.ConfirmPassword)
            return false;
        
        var contact = await repository.GetContactAsync(id, CancellationToken.None);
        if (contact is null)
            return false;
        
        contact.PasswordHash = passwordService.HashPassword(contact, newData.Password);

        return await repository.UpdateContactAsync(id, contact);
    }

    public async Task<bool> DeleteContactAsync(int id) => 
        await repository.DeleteContactAsync(id);

    public async Task<CreateContactResult> CreateContactAsync(ContactCreationDto contactCreationDto)
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
        
        var id = await repository.CreateContactAsync(newContact);
        
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