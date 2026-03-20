namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Represents the response containing geo move compatibility checks
    /// </summary>
    public class GeoMoveCompatibilityResponse
    {
        public GeoMoveTenantCompatibilityCheck[] GeoMoveTenantCompatibilityChecks { get; set; }
    }
}