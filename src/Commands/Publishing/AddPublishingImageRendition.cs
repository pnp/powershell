using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Publishing
{
    [Cmdlet(VerbsCommon.Add, "PnPPublishingImageRendition")]
    
    
    public class AddPublishingImageRendition : PnPWebCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name = string.Empty;

        [Parameter(Mandatory = true)]
        public int Width = 0;
        
        [Parameter(Mandatory = true)]
        public int Height = 0;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.CreatePublishingImageRendition(Name, Width, Height);
        }
    }
}
