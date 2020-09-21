using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsLifecycle.Enable, "PnPSiteClassification")]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.Directory_ReadWrite_All)]
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