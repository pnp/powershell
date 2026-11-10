namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains SharePoint Online multi-geo move compatibility checks.
	/// </summary>
	public class GeoMoveCompatibilityChecks
	{
		/// <summary>
		/// Compatibility checks between source and destination geo locations.
		/// </summary>
		public GeoMoveTenantCompatibilityCheck[] GeoMoveTenantCompatibilityChecks { get; set; }
	}
}