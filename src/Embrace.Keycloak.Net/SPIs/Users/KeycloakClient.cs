using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Models;
using Keycloak.Net.Models.Users;

namespace Keycloak.Net;

public partial class KeycloakClient
{
    public async Task<Response<IEnumerable<User>>> GetUsersWithRole(string realm, IEnumerable<string> roleIds)
    {
        try
        {
             var users = await GetBaseUrl(realm)
                 .AppendPathSegment($"/realms/{realm}/spi-users")
                 .SetQueryParams(new { roleIds = roleIds })
                 .WithTimeout(600)
                 .WithHeader(Constants.HttpConstants.ContentType, Constants.HttpConstants.FormUrlEncoded)
                 .GetJsonAsync<IEnumerable<User>>()
                 .ConfigureAwait(false);
            
            return Response<IEnumerable<User>>.Success(HttpStatusCode.OK, users);
        }
        catch (FlurlHttpException ex)
        {
            return await HandleErrorResponse<IEnumerable<User>>(ex);
        }
    }
    
}