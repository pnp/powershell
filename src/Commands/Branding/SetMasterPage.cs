using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "PnPMasterPage")]
    public class SetMasterPage : PnPWebCmdlet
    {
        private const string ParameterSet_SERVER = "Server Relative";
        private const string ParameterSet_SITE = "Site Relative";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SERVER)]
        public string MasterPageServerRelativeUrl = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SERVER)]
        public string CustomMasterPageServerRelativeUrl = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SITE)]
        public string MasterPageSiteRelativeUrl = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_SITE)]
        public string CustomMasterPageSiteRelativeUrl = null;

        protected override void ExecuteCmdlet()
        {
            if (CurrentWeb.IsNoScriptSite())
            {
                ThrowTerminatingError(new ErrorRecord(new Exception("Site has NoScript enabled, and setting custom master pages is not supported."), "NoScriptEnabled", ErrorCategory.InvalidOperation, this));
                return;
            }

            if (ParameterSetName == ParameterSet_SERVER)
            {
                if (!string.IsNullOrEmpty(MasterPageServerRelativeUrl))
                    CurrentWeb.SetMasterPageByUrl(MasterPageServerRelativeUrl);

                if (!string.IsNullOrEmpty(CustomMasterPageServerRelativeUrl))
                    CurrentWeb.SetCustomMasterPageByUrl(CustomMasterPageServerRelativeUrl);
            }
            else
            {
                if (!string.IsNullOrEmpty(MasterPageSiteRelativeUrl))
                {
                    CurrentWeb.SetMasterPageByUrl(GetServerRelativeUrl(MasterPageSiteRelativeUrl));
                }
                if (!string.IsNullOrEmpty(CustomMasterPageSiteRelativeUrl))
                {
                    CurrentWeb.SetCustomMasterPageByUrl(GetServerRelativeUrl(CustomMasterPageSiteRelativeUrl));
                }
            }
        }

        private string GetServerRelativeUrl(string url)
        {
            var serverRelativeUrl = CurrentWeb.EnsureProperty(w => w.ServerRelativeUrl);
            return UrlUtility.Combine(serverRelativeUrl, url);
        }
    }
}
