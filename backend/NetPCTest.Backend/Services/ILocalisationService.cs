namespace NetPCTest.Backend.Services;

/// <summary>
/// Provides an abstraction of locale-related methods.
/// </summary>
public interface ILocalisationService
{
    /// <summary>
    /// Returns all available locales.
    /// </summary>
    /// <returns>A list of available locales.</returns>
    public Task<List<string>> GetAllLocales();
    
    /// <summary>
    /// Returns all KeyStrings present in a locale.
    /// </summary>
    /// <param name="locale">Locale name.</param>
    /// <returns>Dictionary of translations specific to the specified locale.</returns>
    public Task<Dictionary<string, string>> GetLocaleKeyStrings(string locale);
}