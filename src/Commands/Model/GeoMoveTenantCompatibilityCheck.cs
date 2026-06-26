using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the compatibility status for moving SharePoint Online sites between two geo locations.
	/// </summary>
	public class GeoMoveTenantCompatibilityCheck
	{
		/// <summary>
		/// Source geo location code.
		/// </summary>
		public string SourceDataLocation { get; set; }

		/// <summary>
		/// Destination geo location code.
		/// </summary>
		public string DestinationDataLocation { get; set; }

		/// <summary>
		/// Compatibility status for moves between the source and destination geo locations.
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public GeoMoveCompatibilityValidationResult GeoMoveTenantCompatibilityResult { get; set; }
	}
}