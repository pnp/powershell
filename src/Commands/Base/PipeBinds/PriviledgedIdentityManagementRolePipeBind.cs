using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PriviledgedIdentityManagementRolePipeBind
    {
        public readonly Guid? Id;

        public readonly string DisplayName;

        public RoleDefinition Instance { get; private set; }

        public PriviledgedIdentityManagementRolePipeBind(RoleDefinition instance)
        {
            Instance = instance;
        }

        public PriviledgedIdentityManagementRolePipeBind(Guid id)
        {
            Id = id;
        }

        public PriviledgedIdentityManagementRolePipeBind(string id)
        {
            if(Guid.TryParse(id, out Guid guidId))
            {
                Id = guidId;
            }
            else
            {
                DisplayName = id;
            }
        }

        internal RoleDefinition GetInstance(ApiRequestHelper requestHelper)
        {
            if (Instance != null)
            {
                return Instance;
            }
            if (Id.HasValue)
            {
                Instance = PriviledgedIdentityManagamentUtility.GetRoleDefinitionById(requestHelper, Id.Value);
            }
            if (!string.IsNullOrEmpty(DisplayName))
            {
                Instance = PriviledgedIdentityManagamentUtility.GetRoleDefinitionByName(requestHelper, DisplayName);
            }
            return Instance;
        }
    }
}
