﻿using System;
using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPPriviledgedIdentityManagementEligibleAssignment")]
    [OutputType(typeof(List<RoleEligibilitySchedule>))]
    [OutputType(typeof(RoleEligibilitySchedule))]
    [RequiredMinimalApiPermissions("RoleAssignmentSchedule.Read.Directory")]
    public class GetPriviledgedIdentityManagementEligibleAssignment : PnPGraphCmdlet
    {
        /// <summary>
        /// Specific eligible role to retrieve
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0)]
        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind Identity { get; set; }

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteVerbose("Retrieving specific eligible role assignment");
                var role = Identity.GetInstance(this, Connection, AccessToken);
                WriteObject(role, false);
            }
            else
            {
                WriteVerbose("Retrieving all eligible role assignments");
                var roles = PriviledgedIdentityManagamentUtility.GetRoleEligibilitySchedules(this, Connection, AccessToken);
                WriteObject(roles, true);
            }
        }
    }
}
