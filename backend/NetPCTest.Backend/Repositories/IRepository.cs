using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Repositories;

// We don't use CancellationTokens when it comes to modifying data in order to avoid half-written weirdness.
public interface IRepository
{
    Task<int> GetContactCount(CancellationToken cancellationToken);
    Task<List<ContactBrief>> GetContacts(int startIndex, int count, CancellationToken cancellationToken);
    Task<Contact?> GetContact(int id, CancellationToken cancellationToken);
    Task<Contact?> GetContactByEmail(string email, CancellationToken cancellationToken);
    Task<int?> CreateContact(Contact contact);
    Task<Category?> GetCategory(int id, CancellationToken cancellationToken);
    Task<SubCategory?> GetSubCategory(int id, CancellationToken cancellationToken);
    Task<int> GetCategoryCount(CancellationToken cancellationToken);
    // Beware all ye who dare use this method. It returns the SubCategories too (under each Category).
    Task<List<Category>> GetCategories(CancellationToken cancellationToken);
    Task<bool> UpdateContact(int contactId, Contact contact);
    Task<bool> DeleteContact(int contactId);
}