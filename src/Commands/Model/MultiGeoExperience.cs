using PnP.PowerShell.Commands.Enums;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the SharePoint Online multi-geo experience mode for a geo location.
	/// </summary>
	public class MultiGeoExperience
	{
		/// <summary>
		/// The geo location code.
		/// </summary>
		public string GeoLocation { get; set; }

		/// <summary>
		/// The SharePoint Online multi-geo experience mode.
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public MultiGeoExperienceMode MultiGeoExperienceMode { get; set; }
	}
}
