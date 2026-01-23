using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Remove, "PnPAzureADServicePrincipalAssignedAppRole")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AppRoleAssignment.ReadWrite.All")]
    [OutputType(typeof(List<AzureADServicePrincipalAppRole>))]
    [Alias("Remove-PnPEntraIDServicePrincipalAssignedAppRole")]
    public class RemoveAzureADServicePrincipalAssignedAppRole : PnPGraphCmdlet
    {
        private const string ParameterSet_BYINSTANCE = "By instance";
        private const string ParameterSet_BYASSIGNEDAPPROLE = "By assigned app role";
        private const string ParameterSet_BYAPPROLENAME = "By approle name";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYASSIGNEDAPPROLE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYAPPROLENAME)]
        public ServicePrincipalPipeBind Principal;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYASSIGNEDAPPROLE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYINSTANCE)]
        public ServicePrincipalAssignedAppRoleBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYAPPROLENAME)]
        public string AppRoleName;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSetName == ParameterSet_BYASSIGNEDAPPROLE || ParameterSetName == ParameterSet_BYAPPROLENAME)
            {
                var principal = Principal.GetServicePrincipal(GraphRequestHelper);

                if (principal == null)
                {
                    throw new PSArgumentException("Service principal not found", nameof(Principal));
                }

                LogDebug($"Removing currently assigned app roles from service principal {principal.DisplayName} ({principal.Id})");

                if (ParameterSetName == ParameterSet_BYASSIGNEDAPPROLE)
                {
                    if (ParameterSpecified(nameof(Identity)))
                    {
                        var appRoleAssignment = Identity.GetAssignedAppRole(GraphRequestHelper, principal.Id);
                        ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(GraphRequestHelper, appRoleAssignment);
                    }
                    else
                    {
                        ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(GraphRequestHelper, principal);
                    }
                }
                else
                {
                    ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(GraphRequestHelper, principal, AppRoleName);
                }
            }
            else
            {
                var appRoleAssignment = Identity.GetAssignedAppRole(GraphRequestHelper);
                ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(GraphRequestHelper, appRoleAssignment);
            }
        }
    }
}