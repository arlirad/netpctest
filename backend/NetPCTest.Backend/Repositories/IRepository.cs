using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Repositories;

// We don't use CancellationTokens when it comes to modifying data in order to avoid half-written weirdness.
public interface IRepository
{
    Task<List<ContactBrief>> GetContacts(int startIndex, int count, CancellationToken cancellationToken);
    Task<Contact?> GetContact(int id, CancellationToken cancellationToken);
    Task<Contact?> GetContactByEmail(string email, CancellationToken cancellationToken);
    Task<int?> CreateContact(Contact contact);
    Task<Category?> GetCategory(int id, CancellationToken cancellationToken);
    Task<SubCategory?> GetSubCategory(int id, CancellationToken cancellationToken);
    Task<bool> UpdateContact(int contactId, Contact contact);
}