using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADServicePrincipalAssignedAppRole")]
    [RequiredApiApplicationPermissions("graph/Application.Read.All")]
    [OutputType(typeof(List<AzureADServicePrincipalAppRole>))]
    [Alias("Get-PnPEntraIDServicePrincipalAssignedAppRole")]
    public class GetAzureADServicePrincipalAssignedAppRole : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public ServicePrincipalPipeBind Principal;

        [Parameter(Mandatory = false)]
        public ServicePrincipalAvailableAppRoleBind Identity;

        protected override void ExecuteCmdlet()
        {
            var principal = Principal.GetServicePrincipal(RequestHelper);

            if(principal == null)
            {
                throw new PSArgumentException("Service principal not found", nameof(Principal));
            }

            WriteVerbose($"Requesting currently assigned app roles to service principal {principal.DisplayName}");

            var appRoleAssignments = ServicePrincipalUtility.GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(RequestHelper, principal.Id);
            if(ParameterSpecified(nameof(Identity)))
            {
                var appRole = Identity.GetAvailableAppRole(Connection, AccessToken, principal);
                WriteObject(appRole, false);
            }
            else
            {
                WriteObject(appRoleAssignments, true);
            }
        }
    }
}