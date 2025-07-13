using Microsoft.AspNetCore.Identity;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Services;

public class PasswordService(IPasswordHasher<Contact> passwordHasher) : IPasswordService
{
    public string HashPassword(Contact contact, string password)
        => passwordHasher.HashPassword(contact, password);

    public PasswordVerificationResult ComparePassword(Contact contact, string hashedPassword, string providedPlainPassword)
        => passwordHasher.VerifyHashedPassword(contact, hashedPassword, providedPlainPassword);
}