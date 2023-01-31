using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;

using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Webhooks
{
    [Cmdlet(VerbsCommon.Add, "PnPWebhookSubscription")]
    [OutputType(typeof(WebhookSubscription))]
    public class AddWebhookSubscription : PnPWebCmdlet
    {
        public const int DefaultValidityInDays = 180; // Note: the max is 180 days not 6 months - https://learn.microsoft.com/sharepoint/dev/apis/webhooks/overview-sharepoint-webhooks
        public const int ValidityDeltaInDays = -72; // Note: Some expiration dates too close to the limit are rejected


        [Parameter(Mandatory = false)]
        public ListPipeBind List;

        [Parameter(Mandatory = true)]
        public string NotificationUrl;

        [Parameter(Mandatory = false)]
        public DateTime ExpirationDate = DateTime.Today.ToUniversalTime().AddDays(DefaultValidityInDays).AddHours(ValidityDeltaInDays);

        [Parameter(Mandatory = false)]
        public string ClientState = string.Empty;

        protected override void ExecuteCmdlet()
        {
            // NOTE: Currently only supports List Webhooks
            if (ParameterSpecified(nameof(List)))
            {
                // Get the list from the currently selected web
                List list = List.GetList(CurrentWeb);
                if (list != null)
                {
                    // Ensure we have list Id (TODO Should be changed in the Core extension method)
                    list.EnsureProperty(l => l.Id);

                    // Write the subscription result object
                    WriteObject(list.AddWebhookSubscription(NotificationUrl, ExpirationDate, ClientState));
                }  
            }
            else
            {
                throw new PSNotImplementedException("This Cmdlet only supports List Webhooks currently");
            }
        }

    }
}