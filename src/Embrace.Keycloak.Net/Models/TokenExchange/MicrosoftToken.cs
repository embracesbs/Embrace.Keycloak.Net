using Newtonsoft.Json;

namespace Keycloak.Net.Models.TokenExchange;

public class MicrosoftToken
{
    [JsonProperty("token_type")]
    public string TokenType { get; init; }

    [JsonProperty("scope")]
    public string Scope { get; init; }

    [JsonProperty("access_token")]
    public string AccessToken { get; init; }

    [JsonProperty("id_token")]
    public string IdToken { get; init; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; init; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; init; }

    [JsonProperty("ext_expires_in")]
    public int ExtExpiresIn { get; init; }
}