using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADServicePrincipalAssignedAppRole")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.Read.All")]
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
            var principal = Principal.GetServicePrincipal(GraphRequestHelper);

            if (principal == null)
            {
                throw new PSArgumentException("Service principal not found", nameof(Principal));
            }

            LogDebug($"Requesting currently assigned app roles to service principal {principal.DisplayName}");

            var appRoleAssignments = ServicePrincipalUtility.GetServicePrincipalAppRoleAssignmentsByServicePrincipalObjectId(GraphRequestHelper, principal.Id);
            if (ParameterSpecified(nameof(Identity)))
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