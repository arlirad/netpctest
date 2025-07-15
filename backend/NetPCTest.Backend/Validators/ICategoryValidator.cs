using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Validators;

/// <summary>
/// Defines an abstraction of the validation of <see cref="Category"/> and <see cref="SubCategory"/> rules.
/// </summary>
public interface ICategoryValidator
{
    Task<CategoryValidationResult> Validate(Contact newContact, CancellationToken cancellationToken = default);
}