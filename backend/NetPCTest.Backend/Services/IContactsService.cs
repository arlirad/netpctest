using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Services;

public interface IContactsService
{
    public Task<int> GetContactCount(CancellationToken cancellationToken);
    public Task<List<ContactBriefDto>> GetContacts(int startIndex, int count, CancellationToken cancellationToken);
    Task<CreateContactResult> CreateContact(ContactCreationDto contactCreationDto);
    Task<ContactDto?> GetContact(int id, CancellationToken cancellationToken);
    Task<UpdateContactResult> UpdateContact(int id, ContactUpdateDto newData);
    Task<bool> DeleteContact(int id);
}