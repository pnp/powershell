using System.Collections;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Attributes;
using System.Net.Http.Json;
using System.Linq;
using System.Collections.Generic;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Set, "PnPSearchExternalItem")]
    [RequiredApiApplicationPermissions("graph/ExternalItem.ReadWrite.All")]
    [OutputType(typeof(Model.Graph.MicrosoftSearch.ExternalItem))]
    public class SetSearchExternalItem : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public SearchExternalConnectionPipeBind ConnectionId;

        [Parameter(Mandatory = true)]
        [ValidateLength(1,128)]
        public string ItemId;

        [Parameter(Mandatory = true)]
        public Hashtable Properties;

        #region Content

        [Parameter(Mandatory = false)]
        public string ContentValue;

        [Parameter(Mandatory = false)]
        public Enums.SearchExternalItemContentType ContentType;

        #endregion

        #region ACL

        [Parameter(Mandatory = false)]
        public AzureADUserPipeBind[] GrantUsers;

        [Parameter(Mandatory = false)]
        public AzureADGroupPipeBind[] GrantGroups;

        [Parameter(Mandatory = false)]
        public AzureADUserPipeBind[] DenyUsers;

        [Parameter(Mandatory = false)]
        public AzureADGroupPipeBind[] DenyGroups;

        [Parameter(Mandatory = false)]
        public string[] GrantExternalGroups;

        [Parameter(Mandatory = false)]
        public string[] DenyExternalGroups;

        [Parameter(Mandatory = false)]
        public SwitchParameter GrantEveryone;

        #endregion

        protected override void ExecuteCmdlet()
        {
            var bodyContent = new Model.Graph.MicrosoftSearch.ExternalItem
            {
                Id = ItemId,
                Acls = new(),
                Properties = Properties,
                Content = new() {
                    Type = ContentType,
                    Value = ContentValue
                }
            };

            WriteVerbose($"Adding {(ParameterSpecified(nameof(GrantUsers)) ? GrantUsers.Length : 0)} Grant User ACLs");
            bodyContent.Acls.AddRange(GetUserAcls(GrantUsers, Enums.SearchExternalItemAclAccessType.Grant));

            WriteVerbose($"Adding {(ParameterSpecified(nameof(DenyUsers)) ? DenyUsers.Length : 0)} Deny User ACLs");
            bodyContent.Acls.AddRange(GetUserAcls(DenyUsers, Enums.SearchExternalItemAclAccessType.Deny));

            WriteVerbose($"Adding {(ParameterSpecified(nameof(GrantGroups)) ? GrantGroups.Length : 0)} Grant Group ACLs");
            bodyContent.Acls.AddRange(GetGroupAcls(GrantGroups, Enums.SearchExternalItemAclAccessType.Grant));

            WriteVerbose($"Adding {(ParameterSpecified(nameof(DenyGroups)) ? DenyGroups.Length : 0)} Deny Group ACLs");
            bodyContent.Acls.AddRange(GetGroupAcls(DenyGroups, Enums.SearchExternalItemAclAccessType.Deny));
            
            WriteVerbose($"Adding {(ParameterSpecified(nameof(GrantExternalGroups)) ? GrantExternalGroups.Length : 0)} Grant External Group ACLs");
            bodyContent.Acls.AddRange(GetExternalGroupAcls(GrantExternalGroups, Enums.SearchExternalItemAclAccessType.Grant));

            WriteVerbose($"Adding {(ParameterSpecified(nameof(DenyExternalGroups)) ? DenyExternalGroups.Length : 0)} Deny External Group ACLs");
            bodyContent.Acls.AddRange(GetExternalGroupAcls(DenyExternalGroups, Enums.SearchExternalItemAclAccessType.Deny));

            if(GrantEveryone.ToBool())
            {
                WriteVerbose($"Adding Grant Everyone ACL");
                bodyContent.Acls.Add(new Model.Graph.MicrosoftSearch.ExternalItemAcl
                {
                    Type = Enums.SearchExternalItemAclType.Everyone,
                    AccessType = Enums.SearchExternalItemAclAccessType.Grant,
                    Value = TenantExtensions.GetTenantIdByUrl(Connection.Url, Connection.AzureEnvironment)
                });
            }

            var jsonContent = JsonContent.Create(bodyContent);
            WriteVerbose($"Constructed payload: {jsonContent.ReadAsStringAsync().GetAwaiter().GetResult()}");

            var externalConnectionId = ConnectionId.GetExternalConnectionId(this, Connection, AccessToken) ?? throw new PSArgumentException("No valid external connection specified", nameof(ConnectionId));
            var graphApiUrl = $"v1.0/external/connections/{externalConnectionId}/items/{ItemId}";
            var results = Utilities.REST.GraphHelper.Put<Model.Graph.MicrosoftSearch.ExternalItem>(this, Connection, graphApiUrl, AccessToken, jsonContent);
            WriteObject(results, false);
        }

        private List<Model.Graph.MicrosoftSearch.ExternalItemAcl> GetUserAcls(AzureADUserPipeBind[] users, Enums.SearchExternalItemAclAccessType accessType)
        {
            var acls = new List<Model.Graph.MicrosoftSearch.ExternalItemAcl>();
            if(users == null) return acls;

            foreach (var user in users)
            {
                var userAclId = user.UserId.HasValue ? user.UserId.Value.ToString() : user.GetUser(AccessToken)?.Id.Value.ToString();

                acls.Add(new Model.Graph.MicrosoftSearch.ExternalItemAcl
                {
                    Type = Enums.SearchExternalItemAclType.User,
                    Value = userAclId,
                    AccessType = accessType
                });
            }

            return acls;
        }

        private IEnumerable<Model.Graph.MicrosoftSearch.ExternalItemAcl> GetGroupAcls(AzureADGroupPipeBind[] groups, Enums.SearchExternalItemAclAccessType accessType)
        {
            var acls = new List<Model.Graph.MicrosoftSearch.ExternalItemAcl>();
            if(groups == null) return acls;

            foreach (var group in groups)
            {
                var userAclId = group.GroupId ?? group.GetGroup(this, Connection, AccessToken)?.Id;

                acls.Add(new Model.Graph.MicrosoftSearch.ExternalItemAcl
                {
                    Type = Enums.SearchExternalItemAclType.Group,
                    Value = userAclId,
                    AccessType = accessType
                });
            }

            return acls;
        }

        private IEnumerable<Model.Graph.MicrosoftSearch.ExternalItemAcl> GetExternalGroupAcls(string[] groups, Enums.SearchExternalItemAclAccessType accessType)
        {
            if (groups == null) return new List<Model.Graph.MicrosoftSearch.ExternalItemAcl>();

            return groups.Select(group => new Model.Graph.MicrosoftSearch.ExternalItemAcl
            {
                Type = Enums.SearchExternalItemAclType.ExternalGroup,
                Value = group,
                AccessType = accessType
            }).ToArray(); 
        }
    }
}