namespace NetPCTest.Frontend.Services;

/// <summary>
/// Provides a way to retrieve localised strings.
/// </summary>
public interface ILocalisationService
{
    /// <summary>
    /// Contains available locales.
    /// </summary>
    List<string> AvailableLocales { get; }
    
    /// <summary>
    /// An event that is raised on every locale change.
    /// </summary>
    Action? OnLocaleChanged { get; set; }
    
    /// <summary>
    /// Refreshes available locales asynchronously.
    /// </summary>
    /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
    Task RefreshAvailableLocalesAsync();
    
    /// <summary>
    /// Changes the current locale and requests it's localisation dictionary.
    /// </summary>
    /// <param name="localeName">The name of the locale to change to</param>
    /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
    Task SetLocaleAsync(string localeName);
    
    /// <summary>
    /// Localises a string.
    /// </summary>
    /// <param name="key">Key name to retrieve the localisation of.</param>
    /// <returns>Localised string, or key on failure.</returns>
    string Translate(string key);
}