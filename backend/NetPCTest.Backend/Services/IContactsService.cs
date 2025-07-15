using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Services;

/// <summary>
/// Provides an abstraction for creating, reading, updating and deleting contacts. 
/// </summary>
public interface IContactsService
{
    /// <summary>
    /// Retrieves the count of contacts asynchronously.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing the count of
    /// contacts.</returns>
    public Task<int> GetContactCountAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Retrieves a range of contacts asynchronously.
    /// </summary>
    /// <param name="startIndex">The zero-based index of the first contact to retrieve.</param>
    /// <param name="count">The number of contacts to retrieve.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a list of
    /// <see cref="ContactBriefDto"/></returns>
    /// <remarks>This method retrieves contact based on their position in the data store.</remarks>
    public Task<List<ContactBriefDto>> GetContactsAsync(int startIndex, int count, CancellationToken cancellationToken);
    
    /// <summary>
    /// Creates a contact asynchronously.
    /// </summary>
    /// <param name="contactCreationDto">A <see cref="ContactCreationDto"/> representing new contact data.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="CreateContactResult"/>.</returns>
    Task<CreateContactResult> CreateContactAsync(ContactCreationDto contactCreationDto);
    
    /// <summary>
    /// Retrieves contact details asynchronously.
    /// </summary>
    /// <param name="id">The ID of the contact to retrieve the data of.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="CreateContactResult"/>.</returns>
    Task<ContactDto?> GetContactAsync(int id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Updates a contact asynchronously.
    /// </summary>
    /// <param name="id">The ID of the contact to modify.</param>
    /// <param name="newData"><see cref="ContactUpdateDto"/> containing new contact data.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="UpdateContactResult"/>.</returns>
    Task<UpdateContactResult> UpdateContactAsync(int id, ContactUpdateDto newData);
    
    /// <summary>
    /// Sets the password of a contact asynchronously.
    /// </summary>
    /// <param name="id">The ID of the contact to modify.</param>
    /// <param name="newPassword"><see cref="ContactPasswordChangeDto"/> containing the new password.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="bool"/>.</returns>
    Task<bool> SetContactPasswordAsync(int id, ContactPasswordChangeDto newPassword);
    
    /// <summary>
    /// Deletes a contact asynchronously.
    /// </summary>
    /// <param name="id">The ID of the contact to delete.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="bool"/>.</returns>
    Task<bool> DeleteContactAsync(int id);
}