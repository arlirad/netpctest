using System.Security.Claims;

namespace NetPCTest.Frontend.Services;

/// <summary>
/// Defines an abstraction of authentication.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// An event raised on every authentication state change.
    /// </summary>
    event Func<Task> AuthStateChangedAsync;
    
    /// <summary>
    /// Retrieves a Bearer token using the specified contact credentials asynchronously.
    /// </summary>
    /// <param name="email">Contact email.</param>
    /// <param name="password">Contact password.</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="bool"/>.</returns>
    Task<bool> Login(string email, string password);
    
    /// <summary>
    /// Discards the current Bearer token asynchronously.
    /// </summary>
    /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
    Task Logout();
    
    /// <summary>
    /// Retrieves the current Bearer token asynchronously.
    /// </summary>
    /// <returns>The Bearer token, or null if none is present.</returns>
    Task<string?> GetBearer();
    
    /// <summary>
    /// Checks whether a Bearer token is stored asynchronously.
    /// </summary>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="bool"/>.</returns>
    Task<bool> IsLoggedIn();
    
    /// <summary>
    /// Retrieves the email stored in the currently held Bearer token asynchronously.
    /// </summary>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="string"/>.</returns>
    Task<string?> GetEmail();
    
    /// <summary>
    /// Retrieves the email stored in the currently held Bearer token asynchronously.
    /// </summary>
    /// <returns><see cref="Task"/> representing the asynchronous operation, with a result containing a
    /// <see cref="ClaimsPrincipal"/>.</returns>
    Task<ClaimsPrincipal> GetUser();
}