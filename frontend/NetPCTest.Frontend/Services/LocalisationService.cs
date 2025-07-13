using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using NetPCTest.Frontend.Configuration;

namespace NetPCTest.Frontend.Services;

public class LocalisationService(HttpClient httpClient, IOptions<ApiOptions> apiOptions)
{
    public List<string> AvailableLocales { get; private set; } = [];

    private Locale _currentLocale = new Locale("_", new Dictionary<string, string>());

    public Action? OnLocaleChanged { get; set; }
    
    public async Task RefreshLocalesAsync()
    {
        var request = 
            await httpClient.GetAsync(apiOptions.Value.BaseUrl + "/localisation");
        var locales = await request.Content.ReadFromJsonAsync<List<string>>();

        if (locales != null)
            AvailableLocales = locales;
    }
    
    public async Task SetLocaleAsync(string localeName)
    {
        var request = 
            await httpClient.GetAsync(apiOptions.Value.BaseUrl + $"/localisation/{localeName}");
        var keyStrings = await request.Content.ReadFromJsonAsync<Dictionary<string, string>>();

        if (keyStrings is null)
            return;
        
        _currentLocale = new Locale(localeName, keyStrings);
        
        // We have to make sure if anything is subscribed, thus we use the ? here.
        OnLocaleChanged?.Invoke();
    }

    public string Translate(string key) => _currentLocale.Translate(key) ?? key;

}