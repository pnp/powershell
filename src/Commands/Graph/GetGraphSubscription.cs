
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPGraphSubscription", DefaultParameterSetName = ParameterSet_LIST)]
    [OutputType(typeof(Framework.Graph.Model.Subscription))]

    // Deliberately omitting the CmdletMicrosoftGraphApiPermission attribute as permissions vary largely by the subscription type being used
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