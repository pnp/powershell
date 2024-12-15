﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Remove, "PnPWikiPage", ConfirmImpact = ConfirmImpact.High)]
    public class RemoveWikiPage : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position=0,ValueFromPipeline=true, ParameterSetName = "SERVER")]
        [Alias("PageUrl")]
        public string ServerRelativePageUrl = string.Empty;

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "SITE")]
        public string SiteRelativePageUrl = string.Empty;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "SITE")
            {
                var serverUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
                ServerRelativePageUrl = UrlUtility.Combine(serverUrl, SiteRelativePageUrl);
            }
            var file = CurrentWeb.GetFileByServerRelativeUrl(ServerRelativePageUrl);
            file.DeleteObject();

            ClientContext.ExecuteQueryRetry();
        }
    }
}
