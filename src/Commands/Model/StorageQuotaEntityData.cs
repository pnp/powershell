using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the request data for updating a SharePoint Online multi-geo storage quota.
	/// </summary>
	internal class StorageQuotaEntityData
	{
		public string GeoLocation { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string GeoUsedStorageMB { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string GeoAvailableStorageMB { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string GeoAllocatedStorageMB { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string TenantStorageMB { get; set; }

		public int QuotaType { get; set; }
	}
}
