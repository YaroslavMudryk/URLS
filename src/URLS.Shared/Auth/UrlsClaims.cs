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
        public const string Create = nameof(Create);
        public const string Update = nameof(Update);
        public const string Delete = nameof(Delete);
        public const string SoftDelete = nameof(SoftDelete);
        public const string ViewAll = nameof(ViewAll);
        public const string View = nameof(View);
        public const string ChangeSecret = nameof(ChangeSecret);
    }
}
