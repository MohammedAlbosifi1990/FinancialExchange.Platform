
namespace Shared.Core.Domain.Constants;

public abstract partial class Constants
{
    
    public abstract class Paths
    {
        public const string Root = "Assets";
        public const string Upload = @$"{Root}\Uploads";
        public const string SystemImages = @$"{Root}\SystemImages";
        public const string Attachments = @$"{Root}\Attachments";
    }
}