using NetPCTest.Frontend.Dtos;

namespace NetPCTest.Frontend.Services;

/// <summary>
/// Defines an abstraction of accessing contact data.
/// </summary>
public interface IContactsService
{
    /// <summary>
    /// Retrieves the count of contacts asynchronously.
    /// </summary>
    /// <returns><see cref="Task"/> representing the asynchronous operation. The result of the task is the count of contacts.</returns>
    Task<int> GetContactCountAsync();
    
    /// <summary>
    /// Retrieves contact details asynchronously.
    /// </summary>
    /// <param name="id">The ID of the contact to retrieve the data of.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation. The result of the task is the count of contacts.</returns>
    Task<ContactDto?> GetContactAsync(int id);
    
    /// <summary>
    /// Retrieves a range of contacts asynchronously.
    /// </summary>
    /// <param name="start">The zero-based index of the first contact to retrieve.</param>
    /// <param name="count">The number of contacts to retrieve.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a list of
    /// <see cref="ContactBriefDto"/></returns>
    Task<List<ContactBriefDto>?> GetContactsAsync(int start, int count);
    
    /// <summary>
    /// Creates a contact asynchronously.
    /// </summary>
    /// <param name="newData">A <see cref="ContactCreationDto"/> representing new contact data.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="bool"/>.</returns>
    Task<bool> CreateContactAsync(ContactCreationDto newData);
    
    /// <summary>
    /// Updates a contact asynchronously.
    /// </summary>
    /// <param name="id">The ID of the contact to modify.</param>
    /// <param name="newData"><see cref="ContactUpdateDto"/> containing new contact data.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="bool"/>.</returns>
    Task<bool> UpdateContactAsync(int id, ContactUpdateDto newData);
    
    /// <summary>
    /// Sets the password of a contact asynchronously.
    /// </summary>
    /// <param name="id">The ID of the contact to modify.</param>
    /// <param name="newPassword"><see cref="ContactPasswordChangeDto"/> containing the new password.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="bool"/>.</returns>
    Task<bool> UpdateContactPasswordAsync(int id, ContactPasswordChangeDto newPassword);
    
    /// <summary>
    /// Deletes a contact asynchronously.
    /// </summary>
    /// <param name="id">The ID of the contact to delete.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="bool"/>.</returns>
    Task<bool> DeleteContactAsync(int id);
}