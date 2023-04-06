
using System.Text.RegularExpressions;

namespace Shared.Core.Domain.Extensions;

public static partial class EmailsExtensions
{
    public static bool IsValidEmail(this string email)
    {
        
        try
        {
            if (string.IsNullOrEmpty(email))
                return false;
            var expression = MyRegex();
            var results = expression.IsMatch(email);
            return results;
        }
        catch (Exception)
        {
            return false;
        }
    }

    [GeneratedRegex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$")]
    private static partial Regex MyRegex();
}