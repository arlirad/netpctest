using NetPCTest.Backend.Dtos;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Services;

public interface ILocalisationService
{
    public Task<List<string>> GetAllLocales();
    public Task<List<LocaleKeyStringDto>> GetLocaleKeyStrings(string locale);
}