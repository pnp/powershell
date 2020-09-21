using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Publishing;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Get, "PnPPublishingImageRendition")]
    public class GetPublishingImageRendition : PnPWebCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public ImageRenditionPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Identity.GetImageRendition(SelectedWeb));
            }
            else
            {
                WriteObject(SelectedWeb.GetPublishingImageRenditions(), true);
            }
        }
    }
}
