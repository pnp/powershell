using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Properties of one Microsoft 365 Group
    /// </summary>
    public class Microsoft365Group
    {
        [JsonPropertyName("owners@odata.bind")]
        public string[] OwnersODataBind { get; set; }
        [JsonPropertyName("members@odata.bind")]
        public string[] MembersODataBind { get; set; }
        public Guid? Id { get; set; }

        [JsonIgnore]
        public string GroupId
        {
            get
            {
                return Id.ToString();
            }
        }

        public DateTimeOffset? DeletedDateTime { get; set; }
        public string Classification { get; set; }
        public DateTimeOffset? CreatedDateTime { get; set; }
        public string[] CreationOptions { get; set; }
        public string Description { get; set; }
        public string DisplayName { get; set; }
        public string Mail { get; set; }
        public bool MailEnabled { get; set; }
        public string MailNickname { get; set; }
        public string MembershipRule { get; set; }
        public string MembershipRuleProcessingState { get; set; }
        public string OnPremisesDomainName { get; set; }
        public string OnPremisesNetBiosName { get; set; }
        public DateTimeOffset? OnPremisesLastSyncDateTime { get; set; }
        public string OnPremisesSamAccountName { get; set; }
        public string OnPremisesSecurityIdentifier { get; set; }
        public bool? OnPremisesSyncEnabled { get; set; }
        public string PreferredDataLocation { get; set; }
        public string PreferredLanguage { get; set; }
        public string[] ProxyAddresses { get; set; }

        public DateTimeOffset? RenewedDateTime { get; set; }
        public string[] ResourceBehaviorOptions { get; set; }
        public string[] ResourceProvisioningOptions { get; set; }
        public bool SecurityEnabled { get; set; }
        public string SecurityIdentified { get; set; }
        public string Theme { get; set; }
        public string Visibility { get; set; }
        public string SiteUrl { get; set; }
        public string[] GroupTypes { get; set; }
        public IEnumerable<Microsoft365User> Owners { get; set; }
        public bool? AllowExternalSenders { get; set; }
        public bool? IsSubscribedByMail { get; set; }
        public bool? AutoSubscribeNewMembers { get; set; }

        public List<AssignedLabels> AssignedLabels { get; set; }

        [JsonIgnore]
        public bool HasTeam
        {
            get
            {
                if (ResourceProvisioningOptions != null)
                {
                    return ResourceProvisioningOptions.Contains("Team");
                }
                return false;
            }
        }
    }

    public class AssignedLabels
    {
        public string labelId { get; set; }

        public string displayName { get; set; }
    }
}