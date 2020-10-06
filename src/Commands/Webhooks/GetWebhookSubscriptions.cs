using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Webhooks
{
    [Cmdlet(VerbsCommon.Get, "WebhookSubscriptions")]
    public class GetWebhookSubscriptions : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public ListPipeBind List;

        protected override void ExecuteCmdlet()
        {
            // NOTE: Currently only supports List Webhooks
            if (ParameterSpecified(nameof(List)))
            {
                // Ensure we didn't get piped in a null, i.e. when running Get-PnPList -Identity "ThisListDoesNotExist" | Get-PnPWebhookSubscriptions
                if(List == null)
                {
                    throw new PSArgumentNullException(nameof(List));
                }

                // Get the list from the currently selected web
                List list = List.GetList(SelectedWeb);
                if (list != null)
                {
                    // Get all the webhook subscriptions for the specified list
                    WriteObject(list.GetWebhookSubscriptions());
                }
                else
                {
                    throw new PSArgumentOutOfRangeException(nameof(List), List.ToString(), string.Format(Resources.ListNotFound, List.ToString()));
                }
            }
            else
            {
                throw new PSNotImplementedException(Resources.WebhooksOnlySupportsLists);
            }
        }
    }
}