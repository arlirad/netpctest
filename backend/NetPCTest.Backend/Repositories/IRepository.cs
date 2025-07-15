using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Repositories;

/// <summary>
/// Provides an abstraction of data access to entities. 
/// <remarks><see cref="CancellationToken"/> are not used in methods that modify data in order to avoid issues with
/// half-written data.</remarks>
/// </summary>
public interface IRepository
{
    Task<int> GetContactCountAsync(CancellationToken cancellationToken);
    Task<List<ContactBrief>> GetContactsAsync(int startIndex, int count, CancellationToken cancellationToken);
    Task<Contact?> GetContactAsync(int id, CancellationToken cancellationToken);
    Task<Contact?> GetContactByEmailAsync(string email, CancellationToken cancellationToken);
    Task<int?> CreateContactAsync(Contact contact);
    Task<Category?> GetCategoryAsync(int id, CancellationToken cancellationToken);
    Task<SubCategory?> GetSubCategoryAsync(int id, CancellationToken cancellationToken);
    Task<int> GetCategoryCountAsync(CancellationToken cancellationToken);
    // Beware all ye who dare use this method. It returns the SubCategories too (under each Category).
    Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken);
    Task<bool> UpdateContactAsync(int contactId, Contact contact);
    Task<bool> DeleteContactAsync(int contactId);
}