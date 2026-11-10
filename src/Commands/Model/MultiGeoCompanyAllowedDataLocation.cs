namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains an allowed multi-geo data location configured for the SharePoint Online tenant.
	/// </summary>
	public class MultiGeoCompanyAllowedDataLocation
	{
		/// <summary>
		/// The geo location code, such as NAM or EUR.
		/// </summary>
		public string Location { get; set; }

		/// <summary>
		/// The SharePoint Online domain associated with the geo location.
		/// </summary>
		public string Domain { get; set; }

		/// <summary>
		/// Indicates whether this is the tenant default data location.
		/// </summary>
		public bool IsDefault { get; set; }
	}
}