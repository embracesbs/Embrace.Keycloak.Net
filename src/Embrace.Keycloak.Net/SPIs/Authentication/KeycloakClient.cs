using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Models;
using Keycloak.Net.Models.TokenExchange;
using static Keycloak.Net.Constants;

namespace Keycloak.Net;

public partial class KeycloakClient
{
    public async Task<Response<bool>> NativeSignIn(string email)
    {
        try
        {
            const string realm = "embracecloud";
            await GetBaseUrl(realm)
                .AppendPathSegment($"/realms/{realm}/spi-authentication/native-sign-in")
                .WithHeader(HttpConstants.ContentType, HttpConstants.FormUrlEncoded)
                .PostUrlEncodedAsync(new List<KeyValuePair<string, string>>
                {
                    new("email", email)
                })
                .ConfigureAwait(false);

            return Response<bool>.Success(HttpStatusCode.OK, true);
        }
        catch (FlurlHttpException ex)
        {
            return await HandleErrorResponse<bool>(ex);
        }
    }
        
    public async Task<Response<MsGraphToken>> ExchangeForMsGraphTokenAsync(
        string realm,
        string accessToken,
        string idpAlias)
    {
        try
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/realms/{realm}/spi-authentication/exchange-ms-token")
                .WithHeader(HttpConstants.ContentType, HttpConstants.FormUrlEncoded)
                .PostUrlEncodedAsync(new List<KeyValuePair<string, string>>
                {
                    new(AuthKeywords.IdpAlias, idpAlias),
                    new(AuthKeywords.AccessToken, accessToken)
                })
                .ReceiveJson<MsGraphToken>()
                .ConfigureAwait(false);

            return Response<MsGraphToken>.Success(HttpStatusCode.OK, response);
        }
        catch (FlurlHttpException ex)
        {
            return await HandleErrorResponse<MsGraphToken>(ex);
        }
    }
}