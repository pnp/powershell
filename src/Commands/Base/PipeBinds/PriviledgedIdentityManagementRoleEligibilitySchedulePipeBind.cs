using PnP.PowerShell.Commands.Model.PriviledgedIdentityManagement;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Base.PipeBinds
{
    public sealed class PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind
    {
        private string _displayName;
        private RoleEligibilitySchedule _instance;
        private Guid _id;

        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind(RoleEligibilitySchedule instance)
        {
            _instance = instance;
        }

        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind(Guid id)
        {
            _id = id;
        }

        public PriviledgedIdentityManagementRoleEligibilitySchedulePipeBind(string id)
        {
            if (!Guid.TryParse(id, out _id))
            {
                _displayName = id;
            }
        }

        public Guid Id => _id;

        public string DisplayName => _displayName;

        public RoleEligibilitySchedule Instance => _instance;

        internal RoleEligibilitySchedule GetInstance(PnPConnection connection, string accessToken)
        {
            if (Instance != null)
            {
                _instance = Instance;
            }
            if (Id != Guid.Empty)
            {
                _instance = PriviledgedIdentityManagamentUtility.GetRoleEligibilityScheduleById(Id, connection, accessToken);
            }
            if (!string.IsNullOrEmpty(DisplayName))
            {
                var instances = PriviledgedIdentityManagamentUtility.GetRoleEligibilitySchedules(connection, accessToken);
                _instance = instances.FirstOrDefault(i => i.RoleDefinition.DisplayName == DisplayName);
            }
            return _instance;
        }
    }
}
