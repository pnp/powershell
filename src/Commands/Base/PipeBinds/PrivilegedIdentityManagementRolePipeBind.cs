using PnP.PowerShell.Commands.Model.PrivilegedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PrivilegedIdentityManagementRolePipeBind
    {
        public readonly Guid? Id;

        public readonly string DisplayName;

        public RoleDefinition Instance { get; private set; }

        public PrivilegedIdentityManagementRolePipeBind(RoleDefinition instance)
        {
            Instance = instance;
        }

        public PrivilegedIdentityManagementRolePipeBind(Guid id)
        {
            Id = id;
        }

        public PrivilegedIdentityManagementRolePipeBind(string id)
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
                Instance = PrivilegedIdentityManagementUtility.GetRoleDefinitionById(requestHelper, Id.Value);
            }
            if (!string.IsNullOrEmpty(DisplayName))
            {
                Instance = PrivilegedIdentityManagementUtility.GetRoleDefinitionByName(requestHelper, DisplayName);
            }
            return Instance;
        }
    }
}
