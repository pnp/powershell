using System;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Get, "PnPWebPartXml")]
    [OutputType(typeof(string))]
    public class GetWebPartXml : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true)]
        public ClassicWebPartPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeWebUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!ServerRelativePageUrl.ToLowerInvariant().StartsWith(serverRelativeWebUrl.ToLowerInvariant()))
            {
                ServerRelativePageUrl = UrlUtility.Combine(serverRelativeWebUrl, ServerRelativePageUrl);
            }

            Guid id;
            if (Identity.Id == Guid.Empty)
            {
                var wp = CurrentWeb.GetWebParts(ServerRelativePageUrl).FirstOrDefault(wps => wps.WebPart.Title == Identity.Title);
                if (wp != null)
                {
                    id = wp.Id;
                }
                else
                {
                    throw new Exception($"Web Part with title '{Identity.Title}' cannot be found on page with URL {ServerRelativePageUrl}");
                }
            }
            else
            {
                id = Identity.Id;
            }


            WriteObject(CurrentWeb.GetWebPartXml(id, ServerRelativePageUrl));
        }
    }
}
