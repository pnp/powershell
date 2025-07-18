using System;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public class PowerAppPermissionsProperties
    {
        public string roleName { get; set; }
        public PowerAppPermissionsPrincipal principal { get; set; }
        public string scope { get; set; }
        public string notifyShareTargetOption { get; set; }
        public bool inviteGuestToTenant { get; set; }
        public DateTime createdOn { get; set; }
        public string createdBy { get; set; }
    }

}