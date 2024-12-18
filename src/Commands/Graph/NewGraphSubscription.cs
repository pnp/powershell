using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Base;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.New, "PnPGraphSubscription")]
    [OutputType(typeof(Framework.Graph.Model.Subscription))]

    // Deliberately omitting the CmdletMicrosoftGraphApiPermission attribute as permissions vary largely by the subscription type being used. This means it will not work with an app-only token.
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