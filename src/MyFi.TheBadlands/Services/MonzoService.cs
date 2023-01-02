using MyFi.TheBadlands.Clients;

namespace MyFi.TheBadlands.Services;

public class MonzoService
{
    private readonly MonzoClient _client;

    public MonzoService(MonzoClient client)
    {
        _client = client;
    }

    public async Task ProcessUser(string authCode, Guid userId)
    {
        var authorization = await _client.AuthorizeAsync(authCode);
        var whoami = await _client.WhoAmIAsync(authorization.AccessToken);
    }
}