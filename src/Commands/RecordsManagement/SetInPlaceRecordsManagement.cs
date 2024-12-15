using System.Management.Automation;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.RecordsManagement
{
    [Cmdlet(VerbsCommon.Set, "PnPInPlaceRecordsManagement")]
    public class SetInPlaceRecordsManagement : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Enable or Disable")]
        public bool Enabled;

        protected override void ExecuteCmdlet()
        {
            if (Enabled)
            {
                ClientContext.Site.ActivateInPlaceRecordsManagementFeature();
            }
            else
            {
                ClientContext.Site.DisableInPlaceRecordsManagementFeature();
            }
        }

    }

}
