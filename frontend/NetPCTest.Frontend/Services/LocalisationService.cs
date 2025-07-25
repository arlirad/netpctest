using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using NetPCTest.Frontend.Configuration;

namespace NetPCTest.Frontend.Services;

public class LocalisationService(HttpClient httpClient) : ILocalisationService
{
    public List<string> AvailableLocales { get; private set; } = [];
    public Action? OnLocaleChanged { get; set; }

    private Locale _currentLocale = new Locale("_", new Dictionary<string, string>());
    
    public async Task RefreshAvailableLocalesAsync()
    {
        var locales = 
            await httpClient.GetFromJsonAsync<List<string>>("localisation");

        if (locales != null)
            AvailableLocales = locales;
    }
    
    public async Task SetLocaleAsync(string localeName)
    {
        var keyStrings = 
            await httpClient.GetFromJsonAsync<Dictionary<string, string>>($"localisation/{localeName}");

        if (keyStrings is null)
            return;
        
        _currentLocale = new Locale(localeName, keyStrings);
        
        // We have to make sure if anything is subscribed, thus we use the ? here.
        OnLocaleChanged?.Invoke();
    }

    public string Translate(string key) => _currentLocale.Translate(key) ?? key;

}