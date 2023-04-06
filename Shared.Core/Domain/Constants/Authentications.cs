namespace Shared.Core.Domain.Constants;

public abstract partial class Constants
{
    public abstract class Authentications
    {
        // public class 
        // {
            // public const string Root = "Settings.Root";
            // public const string Edit = "Settings.Edit";
        // } 
    
    
        public const string UserNotExist = "USER_NOT_EXIST";
        public const string UserAccountIsDisabled = "USER_ACCOUNT_IS_DISABLED";
        public const string PhoneIsNotConfirmed = "PHONE_IS_NOT_CONFIRMED";
        public const string EmailIsNotConfirmed = "Email_IS_NOT_CONFIRMED";
        public const string EmailIsAlreadyExist = "EMAIL_ADDRESS_IS_ALREADY_EXIST";
        public const string UserNameIsAlreadyExist = "USER_NAME_ADDRESS_IS_ALREADY_EXIST";
        public const string UserAccountIsLocked = "USER_ACCOUNT_IS_LOCKED";
        public const string UserAccountIsAllowed = "USER_ACCOUNT_IS_ALLOWED";
        public const string InvalidAccessToken = "INVALID_ACCESS_TOKEN";
        public const string InvalidRefreshToken = "INVALID_REFRESH_TOKEN";
        public const string ConfirmationCodeAssignedForPhone = "CONFIRMATION_CODE_ASSIGNED_FOR_PHONE";
        public const string PasswordIsWrong = "PASSWORD_IS_WRONG";
        // public const string ConfirmationCodeTimeIsSpend = "CONFIRMATION_CODE_TIME_IS_SPEND";
        // public const string ConfirmationCodeIsNotEqual = "CONFIRMATION_CODE_IS_NOT_EQUAL";
    }
}