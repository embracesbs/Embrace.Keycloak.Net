using Newtonsoft.Json;

namespace Keycloak.Net.Models.IdentityProviders
{
    public class Config
    {
        [JsonProperty("hideOnLoginPage")]
        public string HideOnLoginPage { get; set; }
        [JsonProperty("clientSecret")]
        public string ClientSecret { get; set; }
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
        [JsonProperty("disableUserInfo")]
        public string DisableUserInfo { get; set; }
        [JsonProperty("jwksUrl")]
        public string UseJwksUrl { get; set; }
        [JsonProperty("authorizationUrl")]
        public string AuthorizationUrl { get; set; }
        [JsonProperty("tokenUrl")]
        public string TokenUrl { get; set; }
        [JsonProperty("logoutUrl")]
        public string LogoutUrl { get; set; }
        [JsonProperty("issuer")]
        public string Issuer { get; set; }
    }
}