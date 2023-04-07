namespace Shared.Core.Domain.Constants;

public class Permissions
{
    public const string Permission = "Application.Permission";

    public class SettingsPermissions
    {
        public const string Root = "Settings.Root";
        public const string Edit = "Settings.Edit";
    }

    public class OfficesPermissions
    {
        public const string Root = "Offices.Root";
        public const string DisplayOffices = "Offices.DisplayOffices";
        public const string DisplayOffice = "Offices.DisplayOffice";
        public const string Add = "Offices.Add";
        public const string Edit = "Offices.Edit";
        public const string Delete = "Offices.Delete";
        public const string Activation = "Offices.Activation";
        public const string DisActivation = "Offices.DisActivation";
    }
}