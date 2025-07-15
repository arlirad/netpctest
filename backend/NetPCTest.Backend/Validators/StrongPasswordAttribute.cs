using System.ComponentModel.DataAnnotations;

namespace NetPCTest.Backend.Validators;

/// <summary>
/// Used to validate passwords.
/// </summary>
public class StrongPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        var password = value as string;

        if (string.IsNullOrEmpty(password))
            return new ValidationResult("Password is required.");

        if (password.Length < 8 ||
            !password.Any(char.IsUpper) ||
            !password.Any(char.IsLower) ||
            !password.Any(char.IsDigit) || 
            password.All(char.IsLetterOrDigit))
        {
            return new ValidationResult("Password must be longer than 8 characters, contain a uppercase character, a lowercase character, a digit and a special character.");
        }

        return ValidationResult.Success;
    }
}