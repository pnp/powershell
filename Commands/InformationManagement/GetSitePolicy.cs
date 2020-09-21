using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.CmdletHelpAttributes;

namespace PnP.PowerShell.Commands.InformationManagement
{
    [Cmdlet(VerbsCommon.Get, "PnPSitePolicy")]
    public class GetSitePolicy : PnPWebCmdlet
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter AllAvailable;

        [Parameter(Mandatory = false)]
        public string Name;

        protected override void ExecuteCmdlet()
        {

            if (!ParameterSpecified(nameof(AllAvailable)) && !ParameterSpecified(nameof(Name)))
            {
                // Return the current applied site policy
                WriteObject(this.SelectedWeb.GetAppliedSitePolicy());
            }
            else
            {
                if (ParameterSpecified(nameof(AllAvailable)))
                {
                    WriteObject(SelectedWeb.GetSitePolicies(),true);
                    return;
                }
                if (ParameterSpecified(nameof(Name)))
                {
                    WriteObject(SelectedWeb.GetSitePolicyByName(Name));
                }
                
            }
        }

    }

}


