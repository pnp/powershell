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

        public OnPremisesExtensionAttributes OnPremisesExtensionAttributes { get; set; }

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

    /// <summary>
    /// Extension attributes 1-15 of one Microsoft 365 Group. Microsoft Graph only populates these for groups that are synchronized from an on-premises Active Directory,
    /// so they are empty for a cloud only group, also when the equivalent CustomAttribute1-15 properties do hold a value in Exchange Online.
    /// </summary>
    public class OnPremisesExtensionAttributes
    {
        public string ExtensionAttribute1 { get; set; }
        public string ExtensionAttribute2 { get; set; }
        public string ExtensionAttribute3 { get; set; }
        public string ExtensionAttribute4 { get; set; }
        public string ExtensionAttribute5 { get; set; }
        public string ExtensionAttribute6 { get; set; }
        public string ExtensionAttribute7 { get; set; }
        public string ExtensionAttribute8 { get; set; }
        public string ExtensionAttribute9 { get; set; }
        public string ExtensionAttribute10 { get; set; }
        public string ExtensionAttribute11 { get; set; }
        public string ExtensionAttribute12 { get; set; }
        public string ExtensionAttribute13 { get; set; }
        public string ExtensionAttribute14 { get; set; }
        public string ExtensionAttribute15 { get; set; }
    }
}