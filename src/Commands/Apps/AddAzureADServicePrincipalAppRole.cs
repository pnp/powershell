using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Apps
{
    [Cmdlet(VerbsCommon.Add, "PnPAzureADServicePrincipalAppRole")]
    [RequiredMinimalApiPermissions("AppRoleAssignment.ReadWrite.All", "Application.Read.All")]
    public class AddAzureADServicePrincipalAppRole : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull]
        public ServicePrincipalPipeBind Principal;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        [ValidateNotNull]
        public ServicePrincipalAppRoleBind AppRole;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNull]
        public ServicePrincipalPipeBind Resource;

        protected override void ExecuteCmdlet()
        {
            var principal = Principal.GetServicePrincipal(Connection, AccessToken);

            if(principal == null)
            {
                throw new PSArgumentException("Service principal not found", nameof(Principal));
            }

            WriteVerbose($"Adding app role to service principal {principal.DisplayName}");

            var resource = Resource.GetServicePrincipal(Connection, AccessToken);

            if(resource == null)
            {
                throw new PSArgumentException("Resource not found", nameof(Resource));
            }

            WriteVerbose($"Adding app role for resource {resource.DisplayName}");

            var appRole = AppRole.GetAppRole(Connection, AccessToken, resource);

            if(appRole == null)
            {
                throw new PSArgumentException("AppRole not found", nameof(AppRole));
            }

            WriteVerbose($"Adding app role {appRole.Value}: {appRole.DisplayName}");

            var response = ServicePrincipalUtility.AddServicePrincipalRoleAssignment(Connection, AccessToken, principal, resource, appRole);
            WriteObject(response, false);
        }
    }
}