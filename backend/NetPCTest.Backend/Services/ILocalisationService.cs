namespace NetPCTest.Backend.Services;

public interface ILocalisationService
{
    public Task<List<string>> GetAllLocales();
    public Task<Dictionary<string, string>> GetLocaleKeyStrings(string locale);
}