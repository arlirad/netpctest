using Microsoft.AspNetCore.Identity;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Services;

/// <summary>
/// Implements <see cref="IPasswordService"/> using <see cref="IPasswordHasher{Contact}"/>.
/// </summary>
/// <param name="passwordHasher">The <see cref="IPasswordHasher{Contact}"/> used for password operations.</param>
public class PasswordService(IPasswordHasher<Contact> passwordHasher) : IPasswordService
{
    public string HashPassword(Contact contact, string password) => 
        passwordHasher.HashPassword(contact, password);

    public PasswordVerificationResult ComparePassword(Contact contact, string hashedPassword, string providedPlainPassword) => 
        passwordHasher.VerifyHashedPassword(contact, hashedPassword, providedPlainPassword);
}