namespace MyFi.TheBadlands.Models.Monzo;

public class MonzoOptions
{
    public string? BaseUrl { get; set; } = "https://api.monzo.com";
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? AuthRedirectUrl { get; set; }
}