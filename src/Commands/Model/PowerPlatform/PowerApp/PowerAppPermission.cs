
namespace PnP.PowerShell.Commands.Model.PowerPlatform.PowerApp
{
    /// <summary>
    /// Definition of a permission set for a Power App
    /// </summary>
    public class PowerAppPermission
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public PowerAppPermissionsProperties Properties { get; set; }
    }
}