using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Services;

public interface ILocalisationService
{
    public Task<List<LocaleKeyString>> GetLocaleKeyStrings(string locale);
}