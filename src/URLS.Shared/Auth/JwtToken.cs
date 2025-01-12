namespace URLS.Shared.Auth;

public class JwtToken
{
    public string Token { get; set; }
    public string LoginId { get; set; }
    public DateTime ExpiredAt { get; set; }
}
