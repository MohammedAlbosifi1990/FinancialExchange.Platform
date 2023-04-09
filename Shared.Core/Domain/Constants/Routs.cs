namespace Shared.Core.Domain.Constants;

public abstract class RoutesConst
{
    private const string RootPrefix = "Api";
    public const string PublicPrefix = $"{RootPrefix}/Public";
    public const string PublicPrefixC = $"{RootPrefix}/Public/[controller]";

    public const string ProtectedPrefix = $"{RootPrefix}/Protected";
    public const string ProtectedPrefixC = $"{ProtectedPrefix}/[controller]";

    public abstract class Authentications
    {
        public const string AuthPrefix = $"{RootPrefix}/Authentications";
        public const string SignIn = $"SignIn";
        public const string SignInByEmail = $"SignInByEmail";
        public const string SignInByUserName = $"SignInByUserName";
        public const string SignInByPhone = $"SignInByPhone";
        public const string SignInByPhoneAndCode = $"SignInByPhoneAndCode";
        public const string Register = $"Register";
        public const string RegisterByEmail = $"RegisterByEmail";
        public const string RegisterByUserName = $"RegisterByUserName";
        public const string RegisterByPhone = $"RegisterByPhone";


        public const string Confirm = $"Confirm";
        public const string ResendConfirmationCode = $"ResendConfirmationCode";
        public const string ChangePassword = $"ChangePassword";
        public const string ResetPassword = $"ResetPassword";
        public const string ForGetPassword = $"ForGetPassword";
        public const string Logout = $"Logout";
    }
}