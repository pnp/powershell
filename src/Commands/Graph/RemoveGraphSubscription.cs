using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Remove, "PnPGraphSubscription")]
    [OutputType(typeof(void))]
    // Deliberately omitting the CmdletMicrosoftGraphApiPermission attribute as permissions vary largely by the subscription type being used
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