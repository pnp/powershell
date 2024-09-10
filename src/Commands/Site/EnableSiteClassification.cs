using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;

using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPSiteClassification")]
    [RequiredMinimalApiPermissions("https://graph.microsoft.com/Directory.ReadWrite.All")]
    [OutputType(typeof(void))]
    public class EnableSiteClassification : PnPGraphCmdlet
    {

        [Parameter(Mandatory = true)]
        public List<string> Classifications;

        [Parameter(Mandatory = true)]
        public string DefaultClassification;

        [Parameter(Mandatory = false)]
        public string UsageGuidelinesUrl = "";

        protected override void ExecuteCmdlet()
        {
            PnP.Framework.Graph.SiteClassificationsUtility.EnableSiteClassifications(AccessToken, Classifications, DefaultClassification, UsageGuidelinesUrl);
        }
    }
}