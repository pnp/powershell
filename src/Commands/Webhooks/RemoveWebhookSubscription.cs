using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Webhooks
{
    [Cmdlet(VerbsCommon.Remove, "PnPWebhookSubscription")]
    [OutputType(typeof(void))]
    public class RemoveWebhookSubscription : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public WebhookSubscriptionPipeBind Identity;

        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Identity != null)
            {
                // NOTE: Currently only supports List Webhooks
                if (ParameterSpecified(nameof(List)))
                {
                    // Get the list from the currently selected web
                    List list = List.GetListOrThrow(nameof(List), CurrentWeb);
                    if (list != null)
                    {
                        // Ensure we have list Id (and Title for the confirm message)
                        list.EnsureProperties(l => l.Id, l => l.Title);

                        // Check the Force switch of ask confirm
                        if (Force
                            || ShouldContinue(string.Format(Properties.Resources.RemoveWebhookSubscription0From1_2,
                                Identity.Id, Properties.Resources.List, list.Title), Properties.Resources.Confirm))
                        {
                            // Remove the Webhook subscription for the specified Id
                            list.RemoveWebhookSubscription(Identity.Subscription);
                        }

                    }
                }
                else
                {
                    throw new PSNotImplementedException("This Cmdlet only supports List Webhooks currently");
                }
            }
        }

    }
}