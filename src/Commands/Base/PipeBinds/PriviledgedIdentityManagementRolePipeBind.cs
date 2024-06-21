using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using System;

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

        internal RoleDefinition GetInstance(PnPConnection connection, string accessToken)
        {
            if (Instance != null)
            {
                return Instance;
            }
            if (Id.HasValue)
            {
                Instance = PriviledgedIdentityManagamentUtility.GetRoleDefinitionById(Id.Value, connection, accessToken);
            }
            if (!string.IsNullOrEmpty(DisplayName))
            {
                Instance = PriviledgedIdentityManagamentUtility.GetRoleDefinitionByName(DisplayName, connection, accessToken);
            }
            return Instance;
        }
    }
}
