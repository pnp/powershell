using Microsoft.SharePoint.Client;
using PnP.Framework.Entities;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Webhooks
{
    [Cmdlet(VerbsCommon.Set, "PnPWebhookSubscription")]
    [OutputType(typeof(WebhookSubscription))]
    public class SetWebhookSubscription : PnPWebCmdlet
    {
        public const int DefaultValidityInDays = 180; // Note: the max is 180 days not 6 months - https://learn.microsoft.com/sharepoint/dev/apis/webhooks/overview-sharepoint-webhooks
        public const int ValidityDeltaInDays = -72; // Note: Some expiration dates too close to the limit are rejected

        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public WebhookSubscriptionPipeBind Subscription;

        [Parameter(Mandatory = false)]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind List;

        [Parameter(Mandatory = false)]
        public string NotificationUrl;

        [Parameter(Mandatory = false)]
        public DateTime ExpirationDate = DateTime.Today.ToUniversalTime().AddDays(DefaultValidityInDays).AddHours(ValidityDeltaInDays);

        protected override void ExecuteCmdlet()
        {
            if (Subscription != null)
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

                        // If the notification Url is specified, override the property of the subscription object
                        if (ParameterSpecified(nameof(NotificationUrl)))
                        {
                            Subscription.Subscription.NotificationUrl = NotificationUrl;
                        }
                        // If the expiration date is specified, override the property of the subscription object
                        if (ParameterSpecified(nameof(ExpirationDate)))
                        {
                            Subscription.Subscription.ExpirationDateTime = ExpirationDate;
                        }

                        // Write the result object (A flag indicating success)
                        WriteObject(list.UpdateWebhookSubscription(Subscription.Subscription));
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