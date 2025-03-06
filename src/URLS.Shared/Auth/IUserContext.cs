using URLS.Shared.Exceptions;

namespace URLS.Shared.Auth;

public interface IUserContext
{
    CurrentUser CurrentUser { get; }

    string CorrelationId { get; }

    TUser AssumeAuthenticated<TUser>() where TUser : CurrentUser;

    string GetBy<TUser>() where TUser : CurrentUser;
}

public class UserContext : IUserContext
{
    public CurrentUser CurrentUser { get; set; } = UninitializedUser.Instance;

    public string CorrelationId { get; set; } = string.Empty;

    public TUser AssumeAuthenticated<TUser>() where TUser : CurrentUser
    {
        if (CurrentUser is TUser user)
            return user;

        throw new UnauthorizedException();
    }

    public string GetBy<TUser>() where TUser : CurrentUser
    {
        if (CurrentUser is BasicAuthenticatedUser user)
            return user.UserId.ToString();

        throw new UnauthorizedException();
    }
}
