

namespace Shared.Core.Domain.Extensions;

public static class GuidExtensions
{
    public static bool NotEmpty(this Guid? guid)
    {
        return !(guid == null || guid == Guid.Empty);
    }
    
    public static bool NotEmpty(this Guid guid)
    {
        return !(guid == Guid.Empty);
    }
    
    
    public static bool NotEmpty(this string strGuid,out Guid t)
    {
        t=Guid.Empty;
        return !string.IsNullOrEmpty(strGuid) && Guid.TryParse(strGuid,out t);
    }

}