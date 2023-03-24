using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using Microsoft.Online.SharePoint.TenantAdministration;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPDisableSpacesActivation")]
    public class SetDisableSpacesActivation : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true)]
        public SPOSitePipeBind Identity;

        [Parameter(Mandatory = true)]
        public SwitchParameter Disable;

        [Parameter(Mandatory = true)]
        public Enums.DisableSpacesScope Scope;

        protected override void ExecuteCmdlet()
        {
            if(Scope == Enums.DisableSpacesScope.Tenant)
            {
                Tenant.DisableSpacesActivation = Disable.ToBool();
            }
            else
            {
                if(!ParameterSpecified(nameof(Identity)) || string.IsNullOrEmpty(Identity.Url))
                {
                    throw new ArgumentNullException($"{nameof(Identity)} must be provided when setting {nameof(Scope)} to Site");
                }
                Tenant.DisableSpacesActivationOnSite(Identity.Url, Disable.ToBool());
            }
            AdminContext.ExecuteQueryRetry();
        }
    }
}
