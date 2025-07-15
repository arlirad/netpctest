namespace NetPCTest.Frontend.Services;

public interface ILocalisationService
{
    List<string> AvailableLocales { get; }
    Action? OnLocaleChanged { get; set; }
    
    Task RefreshLocalesAsync();
    Task SetLocaleAsync(string localeName);
    string Translate(string key);
}