using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.WebParts;
using PnP.Framework.Utilities;
using System;

namespace PnP.PowerShell.Commands.WebParts
{
    [Cmdlet(VerbsCommon.Remove, "PnPWebPart")]
    [OutputType(typeof(void))]
    public class RemoveWebPart : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "ID")]
        public Guid Identity;

        [Parameter(Mandatory = true, ParameterSetName = "NAME")]
        [Alias("Name")]
        public string Title = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            var serverRelativeWebUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);

            if (!ServerRelativePageUrl.ToLowerInvariant().StartsWith(serverRelativeWebUrl.ToLowerInvariant()))
            {
                ServerRelativePageUrl = UrlUtility.Combine(serverRelativeWebUrl, ServerRelativePageUrl);
            }

            if (ParameterSetName == "NAME")
            {
                CurrentWeb.DeleteWebPart(ServerRelativePageUrl, Title);
            }
            else
            {
                var wps = CurrentWeb.GetWebParts(ServerRelativePageUrl);
                var wp = from w in wps where w.Id == Identity select w;
                var webPartDefinitions = wp as WebPartDefinition[] ?? wp.ToArray();
                if(webPartDefinitions.Any())
                {
                    webPartDefinitions.FirstOrDefault().DeleteWebPart();
                    ClientContext.ExecuteQueryRetry();
                }
            }
        }
    }
}
