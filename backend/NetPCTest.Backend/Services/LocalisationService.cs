using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Models;

namespace NetPCTest.Backend.Services;

public class LocalisationService(AppDbContext context) : ILocalisationService
{
    public async Task<List<LocaleKeyString>> GetLocaleKeyStrings(string locale)
    {
        return await context.LocaleKeyStrings.Where(l => l.Locale.Name == locale).ToListAsync(); 
    }
}