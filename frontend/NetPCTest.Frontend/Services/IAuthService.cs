using System.Security.Claims;

namespace NetPCTest.Frontend.Services;

public interface IAuthService
{
    Task<bool> Login(string email, string password);
    Task Logout();
    Task<string?> GetBearer();
    Task<bool> IsLoggedIn();
    Task<string?> GetEmail();
    Task<ClaimsPrincipal> GetUser();
}