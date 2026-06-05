using System.Collections.Generic;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
	/// <summary>
	/// The version policy settings on a document library.
	/// </summary>
	public class ListVersionPolicy
	{
		/// <summary>
		/// Indicates whether versioning is enabled on the library.
		/// </summary>
		public bool VersioningEnabled { get; set; }

		/// <summary>
		/// Indicates whether minor versions are enabled on the library.
		/// </summary>
		public bool? MinorVersionsEnabled { get; set; }

		/// <summary>
		/// Indicates whether AutoExpiration version trimming is enabled.
		/// </summary>
		public bool? EnableAutoExpirationVersionTrim { get; set; }

		/// <summary>
		/// The number of days after which versions expire.
		/// </summary>
		public int? ExpireVersionsAfterDays { get; set; }

		/// <summary>
		/// The maximum number of major versions to keep.
		/// </summary>
		public int? MajorVersionLimit { get; set; }

		/// <summary>
		/// The maximum number of major versions for which minor versions are retained.
		/// </summary>
		public int? MajorWithMinorVersionsLimit { get; set; }

		/// <summary>
		/// The file types for which the version expiration policy applies.
		/// </summary>
		public string[] FileTypesForVersionExpiration { get; set; }

		/// <summary>
		/// The version policy overrides per file type.
		/// </summary>
		public Dictionary<string, ListVersionPolicyFileTypeSettings> VersionPolicyFileTypeOverride { get; set; }
	}

	/// <summary>
	/// The version policy override settings for a file type.
	/// </summary>
	public class ListVersionPolicyFileTypeSettings
	{
		/// <summary>
		/// The name of the file type override.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The file extensions included in the override.
		/// </summary>
		public string[] Extensions { get; set; }

		/// <summary>
		/// Indicates whether AutoExpiration version trimming is enabled for the file type override.
		/// </summary>
		public bool EnableAutoExpirationVersionTrim { get; set; }

		/// <summary>
		/// The number of days after which versions expire for the file type override.
		/// </summary>
		public int? ExpireVersionsAfterDays { get; set; }

		/// <summary>
		/// The maximum number of major versions to keep for the file type override.
		/// </summary>
		public int? MajorVersionLimit { get; set; }

		/// <summary>
		/// The maximum number of major versions for which minor versions are retained for the file type override.
		/// </summary>
		public int? MajorWithMinorVersionsLimit { get; set; }
	}
}