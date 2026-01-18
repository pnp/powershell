using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
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
        [ValidateNotNull]
        public ServicePrincipalAvailableAppRoleBind AppRoleName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYAPPROLENAME)]
        public ServicePrincipalBuiltInType BuiltInType;

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
                    if (!ParameterSpecified(nameof(BuiltInType)))
                    {
                        ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(GraphRequestHelper, principal, AppRoleName.ToString());
                    }
                    else
                    {
                        var resource = ServicePrincipalUtility.GetServicePrincipalByBuiltInType(GraphRequestHelper, BuiltInType);
                        AzureADServicePrincipalAppRole appRole = AppRoleName.GetAvailableAppRole(Connection, AccessToken, resource);

                        if (appRole == null)
                        {
                            throw new PSArgumentException("AppRole not found", nameof(AppRoleName));
                        }
                        LogDebug($"Removing app role {appRole.Value}: {appRole.DisplayName}");
                        ServicePrincipalUtility.RemoveServicePrincipalRoleAssignment(GraphRequestHelper, principal, appRole);
                    }
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