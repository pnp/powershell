namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Indicates whether a SharePoint Online multi-geo move can be performed between locations.
	/// </summary>
	public enum GeoMoveCompatibilityValidationResult
	{
		Compatible = 0,

		Incompatible = 1,

		Warning = 2,

		Error = 3
	}
}