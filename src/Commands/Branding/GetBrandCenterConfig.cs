using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPBrandCenterConfig")]
    [OutputType(typeof(BrandCenterConfiguration))]
    public class GetBrandCenterConfig : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(BrandCenterUtility.GetBrandCenterConfiguration(this, ClientContext), false);
        }
    }
}
