using PnP.PowerShell.Commands.Model.PrivilegedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PrivilegedIdentityManagementRoleEligibilitySchedulePipeBind
    {
        public readonly Guid? Id;

        public RoleEligibilitySchedule Instance { get; private set; }

        public PrivilegedIdentityManagementRoleEligibilitySchedulePipeBind(RoleEligibilitySchedule instance)
        {
            Instance = instance;
        }

        public PrivilegedIdentityManagementRoleEligibilitySchedulePipeBind(Guid id)
        {
            Id = id;
        }

        public PrivilegedIdentityManagementRoleEligibilitySchedulePipeBind(string id)
        {
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid idGuid))
            {
                Id = idGuid;
            }
        }

        internal RoleEligibilitySchedule GetInstance(ApiRequestHelper requestHelper)
        {
            if (Instance != null)
            {
                return Instance;
            }
            if (Id.HasValue)
            {
                Instance = PrivilegedIdentityManagementUtility.GetRoleEligibilityScheduleById(requestHelper, Id.Value);
            }
            return Instance;
        }
    }
}
