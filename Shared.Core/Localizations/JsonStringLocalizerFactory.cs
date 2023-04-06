using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;

namespace Shared.Core.Localizations;

public  class JsonLocalization
{
    public string Key { get; set; }
    public readonly Dictionary<string, string> LocalizedValue = new();

    public JsonLocalization(string key)
    {
        Key = key;
    }
}

public class JsonStringLocalizerFactory : IStringLocalizerFactory
{
    private readonly IMemoryCache _cache;

    public JsonStringLocalizerFactory(IMemoryCache cache)
    {
        _cache = cache;
    }
    public IStringLocalizer Create(Type resourceSource)
    {
        return new JsonStringLocalizer(_cache);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
        return new JsonStringLocalizer(_cache);
    }
}