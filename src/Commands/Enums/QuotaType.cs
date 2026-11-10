namespace PnP.PowerShell.Commands.Enums
{
	/// <summary>
	/// Specifies how SharePoint Online allocates storage quota for a multi-geo location.
	/// </summary>
	public enum QuotaType
	{
		/// <summary>
		/// Storage quota is allocated directly to the geo location.
		/// </summary>
		Allocated = 0,

		/// <summary>
		/// Storage quota is shared across geo locations.
		/// </summary>
		CrossGeoShared = 1
	}
}
