using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Validators;

public interface ICategoryValidator
{
    Task<CategoryValidationResult> Validate(Contact newContact, CancellationToken cancellationToken = default);
}