using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPSiteScript")]
    [OutputType(typeof(void))]
    public class RemoveSiteScript : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantSiteScriptPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (Force || ShouldContinue(Properties.Resources.RemoveSiteScript, Properties.Resources.Confirm))
            {
                foreach(var script in Identity.GetTenantSiteScript(Tenant))
                {
                    WriteVerbose($"Removing site script {script.Title} with id {script.Id}");

                    Tenant.DeleteSiteScript(script.Id);
                }
                AdminContext.ExecuteQueryRetry();
            }
        }
    }
}