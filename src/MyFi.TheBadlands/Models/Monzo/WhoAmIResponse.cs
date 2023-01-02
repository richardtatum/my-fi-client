using System.Text.Json.Serialization;

namespace MyFi.TheBadlands.Models.Monzo;

public class WhoAmIResponse
{
    [JsonPropertyName("authenticated")]
    public bool Authenticated { get; set; }
    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }
}