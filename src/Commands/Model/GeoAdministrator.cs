using PnP.PowerShell.Commands.Enums;
using System;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Contains a SharePoint Online geo administrator.
	/// </summary>
	public class GeoAdministrator
	{
		/// <summary>
		/// The geo location administered by the geo administrator.
		/// </summary>
		public string GeoLocation { get; set; }

		/// <summary>
		/// The login name of the geo administrator.
		/// </summary>
		public string LoginName { get; set; }

		/// <summary>
		/// The display name of the geo administrator.
		/// </summary>
		public string DisplayName { get; set; }

		/// <summary>
		/// The member type of the geo administrator.
		/// </summary>
		public GroupMemberType MemberType { get; set; }

		/// <summary>
		/// The object identifier of the geo administrator.
		/// </summary>
		public Guid ObjectId { get; set; }
	}
}
