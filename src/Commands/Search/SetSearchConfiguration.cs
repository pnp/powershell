using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Search.Administration;

using PnP.PowerShell.Commands.Enums;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands.Search
{
    [Cmdlet(VerbsCommon.Set, "PnPSearchConfiguration")]
    public class SetSearchConfiguration : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Config")]
        public string Configuration;

        [Parameter(Mandatory = true, ParameterSetName = "Path")]
        public string Path;

        [Parameter(Mandatory = false, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public SearchConfigurationScope Scope = SearchConfigurationScope.Web;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == "Path")
            {
                if (!System.IO.Path.IsPathRooted(Path))
                {
                    Path = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, Path);
                }
                Configuration = System.IO.File.ReadAllText(Path);
            }

            Configuration = Configuration.Trim(new char[] { '\uFEFF', '\u200B' });

            switch (Scope)
            {
                case SearchConfigurationScope.Web:
                    {
                        CurrentWeb.SetSearchConfiguration(Configuration);
                        break;
                    }
                case SearchConfigurationScope.Site:
                    {
                        ClientContext.Site.SetSearchConfiguration(Configuration);
                        break;
                    }
                case SearchConfigurationScope.Subscription:
                    {
                        if (!ClientContext.Url.ToLower().Contains("-admin"))
                        {
                            throw new InvalidOperationException(Resources.CurrentSiteIsNoTenantAdminSite);
                        }

                        ClientContext.ImportSearchSettingsConfiguration(Configuration, SearchObjectLevel.SPSiteSubscription);
                        break;
                    }
            }
        }
    }
}
