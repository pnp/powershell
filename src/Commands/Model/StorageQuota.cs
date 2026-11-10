using PnP.PowerShell.Commands.Enums;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains SharePoint Online storage quota details for a multi-geo location.
	/// </summary>
	public class StorageQuota
	{
		/// <summary>
		/// The geo location code.
		/// </summary>
		public string GeoLocation { get; set; }

		/// <summary>
		/// The storage used by the geo location, in megabytes.
		/// </summary>
		public long GeoUsedStorageMB { get; set; }

		/// <summary>
		/// The storage available to the geo location, in megabytes.
		/// </summary>
		public long GeoAvailableStorageMB { get; set; }

		/// <summary>
		/// The storage allocated to the geo location, in megabytes.
		/// </summary>
		public long GeoAllocatedStorageMB { get; set; }

		/// <summary>
		/// The total tenant storage, in megabytes.
		/// </summary>
		public long TenantStorageMB { get; set; }

		/// <summary>
		/// The storage quota type.
		/// </summary>
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public QuotaType QuotaType { get; set; }
	}
}
