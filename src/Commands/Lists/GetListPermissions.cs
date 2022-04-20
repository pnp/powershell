﻿using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.Core.Model.Security;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPListPermissions")]
    [OutputType(typeof(IRoleDefinition))]
    public class GetListPermissions : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = "ByName")]
        public ListPipeBind Identity;

        [Parameter(Mandatory = true)]
        public int PrincipalId;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetListOrThrow(nameof(List), PnPContext);
            WriteObject(list.GetRoleDefinitions(PrincipalId).RequestedItems);
        }
    }
}
