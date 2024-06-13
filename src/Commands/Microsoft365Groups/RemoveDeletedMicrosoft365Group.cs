﻿using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;

namespace PnP.PowerShell.Commands.Microsoft365Groups
{
    [Cmdlet(VerbsCommon.Remove, "PnPDeletedMicrosoft365Group")]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class RemoveDeletedMicrosoft365Group : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public Microsoft365GroupPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            Microsoft365GroupsUtility.PermanentlyDeleteDeletedGroupAsync(this, Connection, Identity.GetDeletedGroupId(this, Connection, AccessToken), AccessToken).GetAwaiter().GetResult();
        }
    }
}