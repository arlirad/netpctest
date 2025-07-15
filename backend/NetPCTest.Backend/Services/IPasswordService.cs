using Microsoft.AspNetCore.Identity;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Services;

/// <summary>
/// Defines methods for securely hashing and verifying passwords.
/// </summary>
public interface IPasswordService
{
    /// <summary>
    /// Hashes a password.
    /// </summary>
    /// <param name="contact"><see cref="Contact"/> for which the password is intended.</param>
    /// <param name="password">Plaintext password</param>
    /// <returns>A <see cref="string"/> containing the hashed password.</returns>
    string HashPassword(Contact contact, string password);
    
    /// <summary>
    /// Verifies a password.
    /// </summary>
    /// <param name="contact"><see cref="Contact"/> for which the comparison is being done.</param>
    /// <param name="hashedPassword">Hashed password.</param>
    /// <param name="providedPlainPassword">Plaintext password.</param>
    /// <returns>A <see cref="PasswordVerificationResult"/> of the comparison.</returns>
    PasswordVerificationResult ComparePassword(Contact contact, string hashedPassword, string providedPlainPassword);
}