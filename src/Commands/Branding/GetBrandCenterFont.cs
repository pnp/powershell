using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Model.SharePoint.BrandCenter;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Get, "PnPBrandCenterFont")]
    [OutputType(typeof(IEnumerable<Font>))]
    public class GetBrandCenterFont : PnPWebCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            WriteObject(BrandCenterUtility.GetFonts(this, ClientContext), true);
        }
    }
}
