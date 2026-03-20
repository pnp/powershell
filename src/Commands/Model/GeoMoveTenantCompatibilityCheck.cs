using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Represents a geo move tenant compatibility check result
    /// </summary>
    public class GeoMoveTenantCompatibilityCheck
    {
        public string SourceDataLocation { get; set; }
        public string DestinationDataLocation { get; set; }
        public GeoMoveTenantCompatibilityResult GeoMoveTenantCompatibilityResult { get; set; }
    }
}