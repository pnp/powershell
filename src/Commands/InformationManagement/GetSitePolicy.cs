using System.Management.Automation;
using Microsoft.SharePoint.Client;

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
                WriteObject(this.CurrentWeb.GetAppliedSitePolicy());
            }
            else
            {
                if (ParameterSpecified(nameof(AllAvailable)))
                {
                    WriteObject(CurrentWeb.GetSitePolicies(),true);
                    return;
                }
                if (ParameterSpecified(nameof(Name)))
                {
                    WriteObject(CurrentWeb.GetSitePolicyByName(Name));
                }
                
            }
        }

    }

}


