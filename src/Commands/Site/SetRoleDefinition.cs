using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Set, "PnPRoleDefinition")]
    [OutputType(typeof(RoleDefinition))]
    public class SetRoleDefinition : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public RoleDefinitionPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string NewRoleName;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public int Order;

        [Parameter(Mandatory = false)]
        public SwitchParameter SelectAll;

        [Parameter(Mandatory = false)]
        public SwitchParameter ClearAll;

        [Parameter(Mandatory = false)]
        public PermissionKind[] Select;

        [Parameter(Mandatory = false)]
        public PermissionKind[] Clear;

        protected override void ExecuteCmdlet()
        {
            var roleDefinition = Identity.GetRoleDefinition(ClientContext.Site);
            try
            {
                var spBasePerm = roleDefinition.BasePermissions;

                if (ParameterSpecified(nameof(SelectAll)) && ParameterSpecified(nameof(ClearAll)))
                {
                    WriteWarning("Cannot SelectAll and ClearAll permissions at the same time");
                    return;
                }

                if (ParameterSpecified(nameof(NewRoleName)))
                {
                    roleDefinition.Name = NewRoleName;
                }

                if (ParameterSpecified(nameof(Description)))
                {
                    roleDefinition.Description = Description;
                }

                if (ParameterSpecified(nameof(Order)))
                {
                    roleDefinition.Order = Order;
                }

                if (ParameterSpecified(nameof(SelectAll)))
                {
                    foreach (PermissionKind flag in Enum.GetValues(typeof(PermissionKind)))
                    {
                        if (flag != PermissionKind.EmptyMask && flag != PermissionKind.FullMask)
                        {
                            spBasePerm.Set(flag);
                        }
                    }
                }

                if (ParameterSpecified(nameof(ClearAll)))
                {
                    spBasePerm.ClearAll();
                }

                if (ParameterSpecified(nameof(Select)))
                {
                    foreach (var flag in Select)
                    {
                        spBasePerm.Set(flag);
                    }
                }

                if (ParameterSpecified(nameof(Clear)))
                {
                    foreach (var flag in Clear)
                    {
                        spBasePerm.Clear(flag);
                    }
                }

                roleDefinition.BasePermissions = spBasePerm;
                roleDefinition.Update();
                ClientContext.ExecuteQueryRetry();
                WriteObject(roleDefinition);
            }
            catch (ServerException e)
            {
                WriteWarning($@"Exception occurred while trying to set the Role Definition: ""{e.Message}"". Will be skipped.");
            }
        }
    }
}