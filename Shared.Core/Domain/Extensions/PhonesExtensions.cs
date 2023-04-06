
using System.Text.RegularExpressions;

namespace Shared.Core.Domain.Extensions;

public static partial class PhonesExtensions
{
    public static bool IsValidPhone(this string phone)
    {
        
        try
        {
            if (string.IsNullOrEmpty(phone))
                return false;
            var expression = MyRegex();
            var results = expression.IsMatch(phone);
            return results;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [GeneratedRegex("([2]{1}[1]{1}[8]{1}[9]{1}[1,2,4]{1}[0-9]{7})|([0]{1}[9]{1}[1,2,4]{1}[0-9]{7})")]
    private static partial Regex MyRegex();
}