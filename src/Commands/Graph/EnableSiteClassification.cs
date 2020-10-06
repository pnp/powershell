
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsLifecycle.Enable, "SiteClassification")]
    [MicrosoftGraphApiPermissionCheck(MicrosoftGraphApiPermission.Directory_ReadWrite_All)]
    [PnPManagementShellScopes("Directory.ReadWrite.All")]
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