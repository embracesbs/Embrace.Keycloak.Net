﻿using Flurl.Http;
using Keycloak.Net.Common.Extensions;
using Keycloak.Net.Models.Common;
using Keycloak.Net.Models.Groups;
using Keycloak.Net.Models.Users;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http.Newtonsoft;

namespace Keycloak.Net
{
    public partial class KeycloakClient
    {
        public async Task<bool> CreateGroupAsync(string realm, Group group, CancellationToken cancellationToken = default)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups")
                .PostJsonAsync(group, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return response.ResponseMessage.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(string realm, int? first = null, int? max = null, string search = null, 
            bool? exact = null, string q = null, bool? briefRepresentation = null, bool? populateHierarchy = null, 
            CancellationToken cancellationToken = default)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(first)] = first,
                [nameof(max)] = max,
                [nameof(search)] = search,
                [nameof(exact)] = exact,
                [nameof(q)] = q,
                [nameof(briefRepresentation)] = briefRepresentation,
                [nameof(populateHierarchy)] = populateHierarchy
            };

            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<Group>>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<int> GetGroupsCountAsync(string realm, string search = null, bool? top = null, CancellationToken cancellationToken = default)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(search)] = search,
                [nameof(top)] = top
            };

            var result = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups/count")
                .SetQueryParams(queryParams)
                .GetJsonAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return Convert.ToInt32(DynamicExtensions.GetFirstPropertyValue(result));
        }

        public async Task<Group> GetGroupAsync(string realm, string groupId, CancellationToken cancellationToken = default)
        {
            var result = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}")
                .GetJsonAsync<Group>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return result;
        }
        
        public async Task<IEnumerable<Group>> GetSubgroupsAsync(string realm, string groupId, int? first = null, int? max = null, 
            bool? briefRepresentation = null, CancellationToken cancellationToken = default)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(first)] = first,
                [nameof(max)] = max,
                [nameof(briefRepresentation)] = briefRepresentation
            };
            
            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/children")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<Group>>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<bool> UpdateGroupAsync(string realm, string groupId, Group group, CancellationToken cancellationToken = default)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}")
                .PutJsonAsync(group, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return response.ResponseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteGroupAsync(string realm, string groupId, CancellationToken cancellationToken = default)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}")
                .DeleteAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return response.ResponseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> SetOrCreateGroupChildAsync(string realm, string groupId, Group group, CancellationToken cancellationToken = default)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/children")
                .PostJsonAsync(group, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
            return response.ResponseMessage.IsSuccessStatusCode;
        }

        public async Task<ManagementPermission> GetGroupClientAuthorizationPermissionsInitializedAsync(string realm, string groupId, CancellationToken cancellationToken = default) => await GetBaseUrl(realm)
            .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/management/permissions")
            .GetJsonAsync<ManagementPermission>(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        public async Task<ManagementPermission> SetGroupClientAuthorizationPermissionsInitializedAsync(string realm, string groupId, ManagementPermission managementPermission, CancellationToken cancellationToken = default) =>
            await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/management/permissions")
                .PutJsonAsync(managementPermission, cancellationToken: cancellationToken)
                .ReceiveJson<ManagementPermission>()
                .ConfigureAwait(false);

        public async Task<IEnumerable<User>> GetGroupUsersAsync(string realm, string groupId, int? first = null, int? max = null, CancellationToken cancellationToken = default)
        {
            var queryParams = new Dictionary<string, object>
            {
                [nameof(first)] = first,
                [nameof(max)] = max
            };

            return await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/groups/{groupId}/members")
                .SetQueryParams(queryParams)
                .GetJsonAsync<IEnumerable<User>>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
