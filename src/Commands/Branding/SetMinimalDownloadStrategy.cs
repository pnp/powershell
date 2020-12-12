using System.Management.Automation;
using Microsoft.SharePoint.Client;


namespace PnP.PowerShell.Commands.Branding
{
    [Cmdlet(VerbsCommon.Set, "PnPMinimalDownloadStrategy")]
    public class SetMinimalDownloadStrategy : PnPWebCmdlet
    {
        [Parameter(ParameterSetName = "On", Mandatory = true)]
        public SwitchParameter On;

        [Parameter(ParameterSetName = "Off", Mandatory = true)]
        public SwitchParameter Off;
         
        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (On)
            {
                SelectedWeb.Features.Add(PnP.Framework.Constants.FeatureId_Web_MinimalDownloadStrategy, Force, FeatureDefinitionScope.None);
            }
            else
            {
                SelectedWeb.Features.Remove(PnP.Framework.Constants.FeatureId_Web_MinimalDownloadStrategy, Force);
            }
            ClientContext.ExecuteQueryRetry();
        }
    }

}
