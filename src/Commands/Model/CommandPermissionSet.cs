using System;
using System.Linq;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Defines permissions which are all required together.
	/// </summary>
	public class CommandPermissionSet
	{
		/// <summary>
		/// Permissions in this set, combined using AND semantics.
		/// </summary>
		public RequiredApiPermission[] Permissions { get; set; } = Array.Empty<RequiredApiPermission>();

		/// <summary>
		/// Formats the permissions in this set.
		/// </summary>
		public override string ToString() => string.Join(" AND ", Permissions.Select(permission => permission.ToString()));
	}
}