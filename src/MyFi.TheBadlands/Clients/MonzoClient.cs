using Microsoft.Extensions.Options;
using MyFi.TheBadlands.Extensions;
using MyFi.TheBadlands.Models.Monzo;

namespace MyFi.TheBadlands.Clients;

public class MonzoClient
{
    private readonly MonzoOptions _options;
    private readonly HttpClient _httpClient;

    public MonzoClient(IOptions<MonzoOptions> options, HttpClient httpClient)
    {
        _options = options.Value;
        _httpClient = httpClient;
    }

    public async Task<AuthorizationResponse?> AuthorizeAsync(string authCode)
    {
        var url = $"{_options.BaseUrl}/oauth2/token";

        var content = new MultipartFormDataContent()
            .AddFormData("grant_type", "authorization_code")
            .AddFormData("client_id", _options.ClientId)
            .AddFormData("client_secret", _options.ClientSecret)
            .AddFormData("redirect_uri", _options.AuthRedirectUrl)
            .AddFormData("code", authCode);

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = content;

        return await ExecuteAsync<AuthorizationResponse>(request);
    }

    public async Task<AuthorizationResponse?> RefreshTokenAsync(string refreshToken)
    {
        var url = $"{_options.BaseUrl}/oauth2/token"
            .AddQueryParameters("grant_type", "refresh_token")
            .AddQueryParameters("client_id", _options.ClientId)
            .AddQueryParameters("client_secret", _options.ClientSecret)
            .AddQueryParameters("refresh_token", refreshToken);

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        return await ExecuteAsync<AuthorizationResponse>(request);
    }

    public async Task<WhoAmIResponse?> WhoAmIAsync(string accessToken)
    {
        var url = $"{_options.BaseUrl}/ping/whoami";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {accessToken}");

        return await ExecuteAsync<WhoAmIResponse>(request);
    }

    private async Task<T?> ExecuteAsync<T>(HttpRequestMessage request)
    {
        var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        if (!response.IsSuccessStatusCode) return default;

        return await response.Content.ReadFromJsonAsync<T?>()
            .ConfigureAwait(false);
    }
}