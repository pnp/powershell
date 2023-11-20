using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADServicePrincipalAvailableAppRole")]
    [RequiredMinimalApiPermissions("Application.Read.All")]
    [OutputType(typeof(List<AzureADServicePrincipalAppRole>))]
    [Alias("Get-PnPEntraIDServicePrincipalAvailableAppRole")]
    public class GetAzureADServicePrincipalAvailableAppRole : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ServicePrincipalPipeBind Principal;

        [Parameter(Mandatory = false)]
        public ServicePrincipalAvailableAppRoleBind Identity;

        protected override void ExecuteCmdlet()
        {
            var principal = Principal.GetServicePrincipal(Connection, AccessToken);

            if(principal == null)
            {
                throw new PSArgumentException("Service principal not found", nameof(Principal));
            }

            WriteVerbose($"Requesting available app roles for service principal {principal.DisplayName}");

            if(ParameterSpecified(nameof(Identity)))
            {
                var appRole = Identity.GetAvailableAppRole(Connection, AccessToken, principal);
                WriteObject(appRole, false);
            }
            else
            {
                WriteObject(principal.AppRoles, true);
            }
        }
    }
}