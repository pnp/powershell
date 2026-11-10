
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPGraphSubscription", DefaultParameterSetName = ParameterSet_LIST)]
    [OutputType(typeof(Framework.Graph.Model.Subscription))]

    // Deliberately not declaring RequiredApi*Permissions attributes: Microsoft Graph requires read permissions on the resource the subscription was created on, which differ per resource, so any fixed set declared here would be inaccurate.
    [ApiPermissionsDependOnResource(
        Remarks = "Microsoft Graph requires the same read permissions on the resource that were needed to create the subscription. Subscriptions created by other applications are only returned when the delegated permission Subscription.Read.All is granted.",
        DocumentationUrl = "https://learn.microsoft.com/graph/api/subscription-list?view=graph-rest-1.0#permissions")]
    public class GetGraphSubscription : PnPGraphCmdlet
    {
        const string ParameterSet_BYID = "Return by specific ID";
        const string ParameterSet_LIST = "Return a list";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        public string Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                PnP.Framework.Graph.Model.Subscription subscription = PnP.Framework.Graph.SubscriptionsUtility.GetSubscription(AccessToken, System.Guid.Parse(Identity));
                WriteObject(subscription);
            }
            else
            {
                List<PnP.Framework.Graph.Model.Subscription> subscriptions = PnP.Framework.Graph.SubscriptionsUtility.ListSubscriptions(AccessToken);
                WriteObject(subscriptions, true);
            }
        }
    }
}