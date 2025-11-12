using System.Globalization;
using System.Text.Json;

public interface IJsonLocalizer
{
    string this[string key] { get; }
    string Get(string key, string? culture = null);
}

public class JsonLocalizer : IJsonLocalizer
{
    private readonly string _folder;
    private readonly string _defaultCulture = "fr";
    private readonly Dictionary<string, Dictionary<string, string>> _cache = new();

    public JsonLocalizer(IWebHostEnvironment env, IConfiguration cfg)
    {
        _folder = Path.Combine(env.ContentRootPath, cfg["Localization:Folder"] ?? "Localization");
        _defaultCulture = cfg["Localization:DefaultCulture"] ?? "fr";
    }

    public string this[string key] => Get(key);

    public string Get(string key, string? culture = null)
    {
        var c = (culture ?? CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)
                .ToLowerInvariant();

        var dict = Load(c);
        if (dict != null && dict.TryGetValue(key, out var val)) return val;

        var fallback = Load(_defaultCulture) ?? new Dictionary<string, string>();
        return fallback.TryGetValue(key, out var val2) ? val2 : key;
    }


    private Dictionary<string, string>? Load(string culture)
    {
        if (_cache.TryGetValue(culture, out var d)) return d;

        var file = Path.Combine(_folder, $"{culture}.json");
        if (!File.Exists(file)) { _cache[culture] = new(); return _cache[culture]; }

        var json = File.ReadAllText(file);
        var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new();
        _cache[culture] = data;
        return data;
    }
}
