using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPGraphSubscription")]
    [OutputType(typeof(void))]

    // Deliberately not declaring RequiredApi*Permissions attributes: Microsoft Graph requires read permissions on the resource the subscription was created on, which differ per resource, so any fixed set declared here would be inaccurate.
    [ApiPermissionsDependOnResource(
        Remarks = "Microsoft Graph requires the same read permissions on the resource that were needed to create the subscription, i.e. Mail.Read to delete a subscription on messages.",
        DocumentationUrl = "https://learn.microsoft.com/graph/api/subscription-delete?view=graph-rest-1.0#permissions")]
    public class RemoveGraphSubscription : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public GraphSubscriptionPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                SubscriptionsUtility.DeleteSubscription(Identity.SubscriptionId, AccessToken);
            }
        }
    }
}