using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Set, "PnPGraphSubscription")]
    [OutputType(typeof(Framework.Graph.Model.Subscription))]
    public class SetGraphSubscription : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public GraphSubscriptionPipeBind Identity;

        [Parameter(Mandatory = true)]
        public DateTime ExpirationDate;

        protected override void ExecuteCmdlet()
        {
            var subscription = SubscriptionsUtility.UpdateSubscription(Identity.SubscriptionId, ExpirationDate, AccessToken);
            WriteObject(subscription);
        }
    }
}