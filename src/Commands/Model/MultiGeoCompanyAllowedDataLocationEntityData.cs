using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the payload used to add an allowed multi-geo data location.
	/// </summary>
	internal class MultiGeoCompanyAllowedDataLocationEntityData
	{
		/// <summary>
		/// The application identifier used by SharePoint Online for the allowed data location.
		/// </summary>
		[JsonPropertyName("appId")]
		public string AppId { get; set; }

		/// <summary>
		/// The initial SharePoint Online domain for the geo location.
		/// </summary>
		[JsonPropertyName("domain")]
		public string Domain { get; set; }

		/// <summary>
		/// The geo location code, such as NAM or EUR.
		/// </summary>
		[JsonPropertyName("location")]
		public string Location { get; set; }
	}
}
