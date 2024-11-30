using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Remove, "PnPAzureADServicePrincipalAssignedAppRole")]
    [RequiredApiApplicationPermissions("graph/AppRoleAssignment.ReadWrite.All")]
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
            if(ParameterSetName == ParameterSet_BYASSIGNEDAPPROLE || ParameterSetName == ParameterSet_BYAPPROLENAME)
            {
                var principal = Principal.GetServicePrincipal(RequestHelper);

                if(principal == null)
                {
                    throw new PSArgumentException("Service principal not found", nameof(Principal));
                }

                WriteVerbose($"Removing currently assigned app roles from service principal {principal.DisplayName} ({principal.Id})");

                if (ParameterSetName == ParameterSet_BYASSIGNEDAPPROLE)
                {
                    if (ParameterSpecified(nameof(Identity)))
                    {
                        var appRoleAssignment = Identity.GetAssignedAppRole(RequestHelper, principal.Id);
                        ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(RequestHelper, appRoleAssignment);
                    }
                    else
                    {
                        ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(RequestHelper, principal);
                    }
                }
                else
                {
                    ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(RequestHelper, principal, AppRoleName);
                }
            }
            else
            {
                var appRoleAssignment = Identity.GetAssignedAppRole(RequestHelper);
                ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(RequestHelper, appRoleAssignment);
            }
        }
    }
}