namespace URLS.Api.Tests.IntegrationInfra;

public class JwtTokenBuilder
{
    private Dictionary<string, List<string>> _claims = new();

    public static JwtTokenBuilder DefaultBuilder() => new JwtTokenBuilder();

    public JwtTokenBuilder WithClaim(string key, List<string> values)
    {
        if (!_claims.ContainsKey(key))
        {
            _claims.Add(key, values);
        }
        else
        {
            _claims[key] = values;
        }

        return this;
    }

    public Dictionary<string, List<string>> Build() => _claims;
}
