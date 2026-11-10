using PnP.PowerShell.Commands.Enums;
using System;
using System.Text.Json.Serialization;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains the payload used to add a SharePoint Online multi-geo administrator.
	/// </summary>
	internal class GeoAdministratorEntityData
	{
		/// <summary>
		/// The login name for the user or group principal.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string LoginName { get; set; }

		/// <summary>
		/// The display name for the principal.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public string DisplayName { get; set; }

		/// <summary>
		/// The type of principal being added.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public GroupMemberType MemberType { get; set; }

		/// <summary>
		/// The object identifier for the principal.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		public Guid ObjectId { get; set; }
	}
}
