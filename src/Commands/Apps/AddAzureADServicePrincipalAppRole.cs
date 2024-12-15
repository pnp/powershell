using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Add, "PnPAzureADServicePrincipalAppRole")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/AppRoleAssignment.ReadWrite.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.Read.All")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.ReadWrite.All")]
    [Alias("Add-PnPEntraIDServicePrincipalAppRole")]
    public class AddAzureADServicePrincipalAppRole : PnPGraphCmdlet
    {
        private const string ParameterSet_BYRESOURCE = "By resource";
        private const string ParameterSet_BYBUILTINTYPE = "By built in type";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYRESOURCE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYBUILTINTYPE)]
        [ValidateNotNull]
        public ServicePrincipalPipeBind Principal;

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYRESOURCE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYBUILTINTYPE)]
        [ValidateNotNull]
        public ServicePrincipalAvailableAppRoleBind AppRole;

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYRESOURCE)]
        public ServicePrincipalPipeBind Resource;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_BYBUILTINTYPE)]
        public ServicePrincipalBuiltInType BuiltInType;

        protected override void ExecuteCmdlet()
        {
            var principal = Principal.GetServicePrincipal(RequestHelper);

            if (principal == null)
            {
                throw new PSArgumentException("Service principal not found", nameof(Principal));
            }

            WriteVerbose($"Adding app role to service principal {principal.DisplayName}");

            AzureADServicePrincipalAppRole appRole;

            if (AppRole.AppRole == null)
            {
                var resource = ParameterSetName == ParameterSet_BYBUILTINTYPE ? ServicePrincipalUtility.GetServicePrincipalByBuiltInType(RequestHelper, BuiltInType) : Resource.GetServicePrincipal(RequestHelper);

                if (resource == null)
                {
                    throw new PSArgumentException("Resource not found", nameof(resource));
                }
                appRole = AppRole.GetAvailableAppRole(Connection, AccessToken, resource);
            }
            else
            {
                appRole = AppRole.AppRole;
            }

            if (appRole == null)
            {
                throw new PSArgumentException("AppRole not found", nameof(AppRole));
            }

            WriteVerbose($"Adding app role {appRole.Value}: {appRole.DisplayName}");

            var response = ServicePrincipalUtility.AddServicePrincipalRoleAssignment(RequestHelper, principal, appRole);
            WriteObject(response, false);
        }
    }
}