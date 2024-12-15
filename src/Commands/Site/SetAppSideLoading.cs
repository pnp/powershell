using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.Framework;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Set, "PnPAppSideLoading")]
    [OutputType(typeof(void))]
    public class SetAppSideLoading : PnPSharePointCmdlet
    {
        [Parameter(ParameterSetName = "On", Mandatory = true)]
        public SwitchParameter On;

        [Parameter(ParameterSetName = "Off", Mandatory = true)]
        public SwitchParameter Off;
        protected override void ExecuteCmdlet()
        {
            if (On)
            {
                ClientContext.Site.ActivateFeature(Constants.FeatureId_Site_AppSideLoading);
            }
            else
            {
                ClientContext.Site.DeactivateFeature(Constants.FeatureId_Site_AppSideLoading);
            }
        }
    }
}
