using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Models;
using Keycloak.Net.Models.Users;

namespace Keycloak.Net;

public partial class KeycloakClient
{
    public async Task<Response<IEnumerable<User>>> GetUsersInRoles(string realm, IEnumerable<string> roleIds)
    {
        try
        {
             var users = await GetBaseUrl(realm)
                 .AppendPathSegment($"/realms/{realm}/spi-users")
                 .SetQueryParams(new { roleIds = roleIds })
                 .GetJsonAsync<IEnumerable<User>>()
                 .ConfigureAwait(false);
            
            return Response<IEnumerable<User>>.Success(HttpStatusCode.OK, users);
        }
        catch (FlurlHttpException ex)
        {
            return await HandleErrorResponse<IEnumerable<User>>(ex);
        }
    }
    
    public async Task<Response<string>> GetUserLastLoginTimestamp(string realm, string userId)
    {
        try
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/realms/{realm}/spi-users/last-login")
                .SetQueryParams(new { userId = userId })
                .GetAsync()
                .ReceiveString()
                .ConfigureAwait(false);
            
            return Response<string>.Success(HttpStatusCode.OK, response);
        }
        catch (FlurlHttpException ex)
        {
            return await HandleErrorResponse<string>(ex);
        }
    }
    
}
