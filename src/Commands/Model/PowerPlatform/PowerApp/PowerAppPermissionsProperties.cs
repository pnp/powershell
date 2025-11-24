using System;

namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    public class PowerAppPermissionsProperties
    {
        public string RoleName { get; set; }
        public PowerAppPermissionsPrincipal Principal { get; set; }
        public string Scope { get; set; }
        public string NotifyShareTargetOption { get; set; }
        public bool InviteGuestToTenant { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}