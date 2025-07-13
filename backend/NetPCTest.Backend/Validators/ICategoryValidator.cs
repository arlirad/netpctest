using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;
using NetPCTest.Backend.Results;

namespace NetPCTest.Backend.Validators;

public interface ICategoryValidator
{
    Task<CreateContactResult?> Validate(Contact newContact, ContactCreationDto contactCreationDto);
}