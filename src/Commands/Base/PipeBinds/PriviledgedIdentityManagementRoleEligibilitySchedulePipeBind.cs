using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using System;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind
    {
        public readonly Guid? Id;

        public RoleEligibilitySchedule Instance { get; private set; }

        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind(RoleEligibilitySchedule instance)
        {
            Instance = instance;
        }

        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind(Guid id)
        {
            Id = id;
        }

        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind(string id)
        {
            if (!string.IsNullOrEmpty(id) && Guid.TryParse(id, out Guid idGuid))
            {
                Id = idGuid;
            }
        }

        internal RoleEligibilitySchedule GetInstance(PnPConnection connection, string accessToken)
        {
            if (Instance != null)
            {
                return Instance;
            }
            if (Id.HasValue)
            {
                Instance = PriviledgedIdentityManagamentUtility.GetRoleEligibilityScheduleById(Id.Value, connection, accessToken);
            }
            return Instance;
        }
    }
}
