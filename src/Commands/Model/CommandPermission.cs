using System;

namespace PnP.PowerShell.Commands.Model
{
	/// <summary>
	/// Describes the API permissions and additional authorization guidance for a cmdlet.
	/// </summary>
	public class CommandPermission
	{
		public string CommandName { get; set; }
		public CommandPermissionSet[] DelegatedPermissions { get; set; } = Array.Empty<CommandPermissionSet>();
		public CommandPermissionSet[] ApplicationPermissions { get; set; } = Array.Empty<CommandPermissionSet>();
		public bool DelegatedAvailable { get; set; }
		public bool ApplicationAvailable { get; set; }
		public string PermissionSource { get; set; }
		public string[] AdditionalRoles { get; set; } = Array.Empty<string>();
		public string Guidance { get; set; }
	}
}