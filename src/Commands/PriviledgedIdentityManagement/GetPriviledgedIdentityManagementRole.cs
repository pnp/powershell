using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPPriviledgedIdentityManagementRole")]
    [OutputType(typeof(List<RoleDefinition>))]
    [OutputType(typeof(RoleDefinition))]
    [RequiredMinimalApiPermissions("RoleManagement.Read.Directory")]
    public class GetPriviledgedIdentityManagementRole : PnPGraphCmdlet
    {
        /// <summary>
        /// Specific role to retrieve
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public PriviledgedIdentityManagementRolePipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteVerbose("Retrieving specific role");
                var role = Identity.GetInstance(Connection, AccessToken);
                WriteObject(role, false);
            }
            else
            {
                WriteVerbose("Retrieving all roles");
                var roles = PriviledgedIdentityManagamentUtility.GetRoleDefinitions(Connection, AccessToken);
                WriteObject(roles, true);
            }
        }
    }
}
