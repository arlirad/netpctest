using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Services;

public interface IContactsService
{
    Task<List<ContactBriefDto>> GetContacts(int startIndex, int count);
    Task<CreateContactResult> CreateContact(ContactCreationDto contactCreationDto);
    string HashPassword(Contact contact, string password);
    bool ComparePassword(Contact contact, string hashedPassword, string providedPlainPassword);
}