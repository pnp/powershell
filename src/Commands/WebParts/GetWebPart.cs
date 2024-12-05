using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WebParts;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Get, "PnPWebPart")]
    [OutputType(typeof(WebPartDefinition))]
    public class GetWebPart : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public ClassicWebPartPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeWebUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!ServerRelativePageUrl.ToLowerInvariant().StartsWith(serverRelativeWebUrl.ToLowerInvariant()))
            {
                ServerRelativePageUrl = UrlUtility.Combine(serverRelativeWebUrl, ServerRelativePageUrl);
            }


            var definitions = CurrentWeb.GetWebParts(ServerRelativePageUrl);

            if (Identity != null)
            {
                if (Identity.Id != Guid.Empty)
                {
                    var wpfound = from wp in definitions where wp.Id == Identity.Id select wp;
                    var webPartDefinitions = wpfound as WebPartDefinition[] ?? wpfound.ToArray();
                    if (webPartDefinitions.Any())
                    {
                        WriteObject(webPartDefinitions.FirstOrDefault());

                    }
                }
                else if (!string.IsNullOrEmpty(Identity.Title))
                {
                    var wpfound = from wp in definitions where wp.WebPart.Title == Identity.Title select wp;
                    var webPartDefinitions = wpfound as WebPartDefinition[] ?? wpfound.ToArray();
                    if (webPartDefinitions.Any())
                    {
                        WriteObject(webPartDefinitions.FirstOrDefault());
                    }
                }
            }
            else
            {
                WriteObject(definitions, true);
            }
        }
    }
}
