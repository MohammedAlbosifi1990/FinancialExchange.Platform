using System.Globalization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace Shared.Core.Localizations;


public class JsonStringLocalizer : IStringLocalizer
{
    private readonly HashSet<JsonLocalization> _localization=new();

    public JsonStringLocalizer(IMemoryCache cache)
    {
        if (cache.TryGetValue("SystemLocalization", out string? data))
        {
            if (!string.IsNullOrEmpty(data))
            {
                var des = JsonConvert.DeserializeObject<HashSet<JsonLocalization>>(data);
                if (des == null)
                    data = null;
                else
                    _localization =des;
            }
        }

        if (!string.IsNullOrEmpty(data))
            return;

        var result = GetData();
        if (result==null) return;
        
        _localization = result;
        
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            // .SetSlidingExpiration(TimeSpan.FromHours(60))
            // .SetAbsoluteExpiration(TimeSpan.FromHours(3600))
            .SetPriority(CacheItemPriority.High)
            .SetSize(1024);
        cache.Set("Localization", JsonConvert.SerializeObject(_localization),cacheEntryOptions);
    }

    private HashSet<JsonLocalization>? GetData()
    {
        var directoryInfo = new DirectoryInfo(@"Resources");
        if (directoryInfo == null)
            throw new Exception("The Resources Path Not Found");

        var files = directoryInfo.GetFiles("*.json");
        if (!files.Any())
            return null;
        
        HashSet<JsonLocalization> hashSet = new();
        foreach (var fileInfo in files)
        {
            var content= File.ReadAllText(fileInfo.FullName);
            if (string.IsNullOrEmpty(content)) continue;
            
            var list = JsonConvert.DeserializeObject<List<JsonLocalization>>(content)!;
            foreach (var jsonLocalization in list)
                hashSet.Add(jsonLocalization);
        }

        return hashSet;
    }

    public LocalizedString this[string name]
    {
        get
        {
            var value = GetString(name);
            return new LocalizedString(name, value, false);
        }
    }

    public LocalizedString this[string name, params object[] arguments]
    {
        get
        {
            var format = GetString(name);
            var value = string.Format(format, arguments);
            return new LocalizedString(name, value, false);
        }
    }
    
    public LocalizedString this[string name, string culture]
    {
        get
        {
            var cultureInfo = new CultureInfo(culture);
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            var value = GetString(name);
            return new LocalizedString(name, value, false);
        }
    }
    
    public LocalizedString this[string name, string culture, params object[] arguments]
    {
        get
        {
            var cultureInfo = new CultureInfo(culture);

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            var format = GetString(name);
            var value = string.Format(format, arguments);
            return new LocalizedString(name, value, false);
        }
    }

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        return _localization.Where(l => l.LocalizedValue.Keys.Any(lv => lv == CultureInfo.CurrentCulture.Name))
            .Select(l => new LocalizedString(l.Key, l.LocalizedValue[CultureInfo.CurrentCulture.Name], true));
    }
    
    private string GetString(string name)
    {
        var query = _localization.Where(l => l.LocalizedValue.Keys.Any(lv => lv.Contains( CultureInfo.CurrentCulture.Name)));
        var value = query.FirstOrDefault(l => l.Key == name);
        if (value == null)
            return name;
        var content = value.LocalizedValue[CultureInfo.CurrentCulture.Name];
        
        return string.IsNullOrEmpty(content) ? name : content ;
    }
}