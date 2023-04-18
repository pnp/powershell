using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Remove, "PnPListDesign")]
    [OutputType(typeof(void))]
    public class RemoveListDesign : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public TenantListDesignPipeBind Identity;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        [Parameter(Mandatory = false)]
        public SwitchParameter WhatIf;

        protected override void ExecuteCmdlet()
        {
            WriteVerbose("Looking up list design based on the provided identity");
            var listDesigns = Identity.GetTenantListDesign(Tenant);

            if(listDesigns == null || listDesigns.Length == 0)
            {
                throw new PSArgumentException("List design provided through the Identity parameter could not be found", nameof(Identity));
            }

            foreach (var listDesign in listDesigns)
            {
                if (Force || ShouldContinue(Properties.Resources.RemoveListDesign, Properties.Resources.Confirm))
                {
                    if(WhatIf.ToBool())
                    {                        
                        WriteVerbose($"Would remove list design with id {listDesign.Id} if {nameof(WhatIf)} was not present");
                    }
                    else
                    {
                        WriteVerbose($"Removing list design with id {listDesign.Id}");
                        Tenant.RemoveListDesign(listDesign.Id);
                        AdminContext.ExecuteQueryRetry();
                    }
                }
            }
        }
    }
}