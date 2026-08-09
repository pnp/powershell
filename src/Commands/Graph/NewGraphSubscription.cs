using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.New, "PnPGraphSubscription")]
    [OutputType(typeof(Framework.Graph.Model.Subscription))]

    // Deliberately not declaring RequiredApi*Permissions attributes: Microsoft Graph requires read permissions on the resource being subscribed to, which differ per resource, so any fixed set declared here would be inaccurate.
    [ApiPermissionsDependOnResource(
        ParameterName = nameof(Resource),
        Remarks = "Microsoft Graph requires read permissions on the resource being subscribed to, i.e. Mail.Read to subscribe to messages, Sites.Read.All to subscribe to a SharePoint list or Group.Read.All to subscribe to groups.",
        DocumentationUrl = "https://learn.microsoft.com/graph/api/subscription-post-subscriptions?view=graph-rest-1.0#permissions")]
    public class NewGraphSubscription : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PnP.Framework.Enums.GraphSubscriptionChangeType ChangeType;

        [Parameter(Mandatory = true)]
        public String NotificationUrl;

        [Parameter(Mandatory = true)]
        public String Resource;

        [Parameter(Mandatory = false)]
        public DateTime ExpirationDateTime;

        [Parameter(Mandatory = false)]
        public String ClientState;

        [Parameter(Mandatory = false)]
        public PnP.Framework.Enums.GraphSubscriptionTlsVersion LatestSupportedTlsVersion = PnP.Framework.Enums.GraphSubscriptionTlsVersion.v1_2;

        protected override void ExecuteCmdlet()
        {
            var subscription = SubscriptionsUtility.CreateSubscription(
                changeType: ChangeType,
                notificationUrl: NotificationUrl,
                resource: Resource,
                expirationDateTime: ExpirationDateTime,
                clientState: ClientState,
                accessToken: AccessToken,
                latestSupportedTlsVersion: ParameterSpecified(nameof(LatestSupportedTlsVersion)) ? LatestSupportedTlsVersion : default);

            WriteObject(subscription);
        }
    }
}