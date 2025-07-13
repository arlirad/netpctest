using Microsoft.AspNetCore.Identity;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Services;

public interface IPasswordService
{
    string HashPassword(Contact contact, string password);
    PasswordVerificationResult ComparePassword(Contact contact, string hashedPassword, string providedPlainPassword);
}