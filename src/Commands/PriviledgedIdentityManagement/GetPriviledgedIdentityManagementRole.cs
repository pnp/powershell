﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPPriviledgedIdentityManagementRole")]
    [OutputType(typeof(List<RoleDefinition>))]
    [OutputType(typeof(RoleDefinition))]
    [RequiredApiDelegatedOrApplicationPermissions("graph/RoleManagement.Read.Directory")]
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
                LogDebug("Retrieving specific role");
                var role = Identity.GetInstance(GraphRequestHelper);
                WriteObject(role, false);
            }
            else
            {
                LogDebug("Retrieving all roles");
                var roles = PriviledgedIdentityManagamentUtility.GetRoleDefinitions(GraphRequestHelper);
                WriteObject(roles, true);
            }
        }
    }
}
