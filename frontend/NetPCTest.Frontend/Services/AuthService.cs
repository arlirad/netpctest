using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using NetPCTest.Frontend.Dtos;
using NetPCTest.Frontend.Providers;

namespace NetPCTest.Frontend.Services;

public class AuthService(
    HttpClient httpClient,
    ILocalStorageService localStorage,
    AuthenticationStateProvider authStateProvider) : IAuthService
{
    public async Task<bool> Login(string email, string password)
    {
        var response = await httpClient.PostAsJsonAsync("auth", new LoginDto()
        {
            Email = email, 
            Password = password,
        });

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<LoginTokenDto>();
        if (result is null)
            return false;

        await localStorage.SetItemAsync("authToken", result.Token);

        if (authStateProvider is AppAuthStateProvider customProvider)
            customProvider.NotifyUserAuthentication(result.Token);

        return true;
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync("authToken");

        if (authStateProvider is AppAuthStateProvider customProvider)
            customProvider.NotifyUserLogout();
    }

    public async Task<string?> GetBearer() => await localStorage.GetItemAsync<string>("authToken");
    public async Task<bool> IsLoggedIn() => !string.IsNullOrEmpty(await GetBearer());
    
    public async Task<ClaimsPrincipal> GetUser()
    {
        var token = await GetBearer();

        if (string.IsNullOrWhiteSpace(token))
            return new ClaimsPrincipal(new ClaimsIdentity());

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var identity = new ClaimsIdentity(jwt.Claims, "jwt");
        return new ClaimsPrincipal(identity);
    }

    public async Task<string?> GetEmail()
    {
        var user = await GetUser();
        return user.Identity?.Name ?? user.FindFirst("unique_name")?.Value;
    }
}