namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains SharePoint Online geo administrators returned by the multi-geo REST API.
	/// </summary>
	internal class GeoAdministratorCollection
	{
		/// <summary>
		/// The SharePoint Online geo administrators.
		/// </summary>
		public GeoAdministrator[] GeoAdministrators { get; set; }
	}
}
