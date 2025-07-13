namespace NetPCTest.Frontend.Services;

public class Locale(string name, Dictionary<string, string> keyStrings)
{
    public string Name { get; init; } = name;

    public string? Translate(string key) => keyStrings.GetValueOrDefault(key);
}