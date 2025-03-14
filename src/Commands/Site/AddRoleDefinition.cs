using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.PipeBinds;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Site
{
    [Cmdlet(VerbsCommon.Add, "PnPRoleDefinition")]
    [OutputType(typeof(RoleDefinition))]
    public class AddRoleDefinition : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string RoleName;

        [Parameter(Mandatory = false)]
        public RoleDefinitionPipeBind Clone;

        [Parameter(Mandatory = false)]
        public PermissionKind[] Include;

        [Parameter(Mandatory = false)]
        public PermissionKind[] Exclude;

        [Parameter(Mandatory = false)]
        public string Description;

        protected override void ExecuteCmdlet()
        {
            // Validate user inputs
            RoleDefinition roleDefinition = null;
            try
            {
                roleDefinition = ClientContext.Site.RootWeb.RoleDefinitions.GetByName(RoleName);
                ClientContext.Load(roleDefinition);
                ClientContext.ExecuteQueryRetry();
            }
            catch { }
            if (roleDefinition.ServerObjectIsNull == null)
            {
                var spRoleDef = new RoleDefinitionCreationInformation();
                var spBasePerm = new BasePermissions();

                if (ParameterSpecified(nameof(Clone)))
                {
                    var clonePerm = Clone.GetRoleDefinition(ClientContext.Site);
                    spBasePerm = clonePerm.BasePermissions;
                }

                // Include and Exclude Flags
                if (ParameterSpecified(nameof(Include)))
                {
                    foreach (var flag in Include)
                    {
                        spBasePerm.Set(flag);
                    }
                }
                if (ParameterSpecified(nameof(Exclude)))
                {
                    foreach (var flag in Exclude)
                    {
                        spBasePerm.Clear(flag);
                    }
                }

                // Create Role Definition
                spRoleDef.Name = RoleName;
                spRoleDef.Description = Description;
                spRoleDef.BasePermissions = spBasePerm;
                roleDefinition = ClientContext.Site.RootWeb.RoleDefinitions.Add(spRoleDef);
                ClientContext.Load(roleDefinition);
                ClientContext.ExecuteQueryRetry();
                WriteObject(roleDefinition);
            }
            else
            {
                LogWarning($"Unable to add Role Definition as there is an existing role definition with the same name. Will be skipped.");
            }
        }
    }
}
