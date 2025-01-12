using System.Security.Claims;
namespace URLS.Shared.Auth;

public static class UrlsClaims
{
    public static class Types
    {
        public const string UserId = ClaimTypes.NameIdentifier;
        public const string SessionId = "SessionId";
        public const string Login = "Login";
        public const string Role = "Role";

        public const string App = nameof(App);

        public const string University = nameof(University);
        public const string Faculty = nameof(Faculty);
        public const string Specialty = nameof(Specialty);
        public const string Group = nameof(Group);
    }

    public static class Values
    {
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";
        public const string SoftDelete = "SoftDelete";
        public const string ViewAll = "ViewAll";
        public const string View = "View";
    }
}
