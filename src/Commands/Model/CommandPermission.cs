using System;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model
{
    /// <summary>
    /// Describes the API permissions and additional authorization requirements of a cmdlet
    /// </summary>
    public class CommandPermission
    {
        /// <summary>
        /// Name of the cmdlet these permissions apply to
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// Aliases under which this cmdlet can also be called
        /// </summary>
        public string[] Aliases { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Delegated permissions required to run this cmdlet. The permissions within one set are all required, the sets are alternatives to each other.
        /// </summary>
        public CommandPermissionSet[] DelegatedPermissions { get; set; } = Array.Empty<CommandPermissionSet>();

        /// <summary>
        /// Application permissions required to run this cmdlet. The permissions within one set are all required, the sets are alternatives to each other.
        /// </summary>
        public CommandPermissionSet[] ApplicationPermissions { get; set; } = Array.Empty<CommandPermissionSet>();

        /// <summary>
        /// The APIs this cmdlet requires permissions on. Also populated for cmdlets of which the exact scopes depend on the resource they are pointed at at runtime.
        /// </summary>
        public ResourceTypeName[] ResourceTypes { get; set; } = Array.Empty<ResourceTypeName>();

        /// <summary>
        /// Indicates if this cmdlet can be run using a delegated access token. NULL when that could not be determined, which is the case when no permissions are
        /// reported to base it on, i.e. for a cmdlet of which the permissions depend on the resource it is pointed at.
        /// </summary>
        public bool? DelegatedAvailable { get; set; }

        /// <summary>
        /// Indicates if this cmdlet can be run using an application access token. NULL when that could not be determined, which is the case when no permissions are
        /// reported to base it on, i.e. for a cmdlet of which the permissions depend on the resource it is pointed at.
        /// </summary>
        public bool? ApplicationAvailable { get; set; }

        /// <summary>
        /// Indicates where the permissions in this instance originate from and therefore how authoritative they are
        /// </summary>
        public CommandPermissionSource PermissionSource { get; set; }

        /// <summary>
        /// The minimum SharePoint role or permission level needed on the resource this cmdlet acts on. This applies when connecting delegated, where the signed in user is bound
        /// by their own rights, and when connecting app only using Sites.Selected. A tenant wide application permission such as Sites.ReadWrite.All already grants access to every
        /// site, so in that case no role has to be assigned on the site itself.
        /// </summary>
        public SharePointMinimumRole MinimumSharePointRole { get; set; }

        /// <summary>
        /// Roles which need to be held next to the API permissions to be able to run this cmdlet. Each entry states in which connection scenario it applies.
        /// </summary>
        public string[] AdditionalRoles { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Remarks on how to interpret the permissions in this instance
        /// </summary>
        public string Guidance { get; set; }
    }
}
