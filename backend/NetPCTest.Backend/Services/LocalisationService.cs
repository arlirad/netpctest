using Microsoft.EntityFrameworkCore;
using NetPCTest.Backend.Data;
using NetPCTest.Backend.Dtos;

namespace NetPCTest.Backend.Services;

/// <summary>
/// A concrete implementation of <see cref="ILocalisationService"/> utilizing <see cref="AppDbContext"/>.
/// </summary>
/// <param name="context">The <see cref="AppDbContext"/> used for querying localisation data.</param>
public class LocalisationService(AppDbContext context) : ILocalisationService
{
    public async Task<List<string>> GetAllLocales()
    {
        return await context.Locales.Select(l => l.Name).ToListAsync();
    }

    public async Task<Dictionary<string, string>> GetLocaleKeyStrings(string locale)
    {
        return await context.LocaleKeyStrings
            .Where(l => l.Locale.Name == locale)
            .Select(l => new LocaleKeyStringDto{Key = l.Key, Value = l.Value})
            .ToDictionaryAsync(k => k.Key, v => v.Value); 
    }
}