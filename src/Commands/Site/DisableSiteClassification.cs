using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;

using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsLifecycle.Disable, "PnPSiteClassification")]
    [RequiredApiApplicationPermissions("graph/Directory.ReadWrite.All")]
    [OutputType(typeof(void))]
    public class DisableSiteClassification : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            try
            {
                PnP.Framework.Graph.SiteClassificationsUtility.DisableSiteClassifications(AccessToken);
            }
            catch (ApplicationException ex)
            {
                if (ex.Message == @"Missing DirectorySettingTemplate for ""Group.Unified""")
                {
                    // swallow exception
                }
                else
                {
                    throw;
                }
            }
        }
    }
}