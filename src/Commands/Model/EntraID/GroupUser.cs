using System;
using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model.EntraID
{
    /// <summary>
    /// Defines an user or group located in a Entra ID Group
    /// </summary>
    public class GroupUser
    {
        /// <summary>
        /// Group user's user principal name
        /// </summary>
        public string UserPrincipalName { get; set; }

        /// <summary>
        /// Group user's display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Indication if this entry represents a user or a group
        /// </summary>
        public GroupUserType Type { get; set; }

        /// <summary>
        /// Creates a PnP PowerShell EntraIDGroupUser entity from a PnP Framework GroupUser entity
        /// </summary>
        /// <param name="entity">PnP Framework GroupUser entity</param>
        /// <returns>PnP PowerShell EntraIDGroupUser entity</returns>
        internal static GroupUser CreateFrom(PnP.Framework.Entities.GroupUser entity)
        {
            var o = new GroupUser
            {
                UserPrincipalName = entity.UserPrincipalName,
                DisplayName = entity.DisplayName,
                Type = (GroupUserType) Enum.Parse(typeof(GroupUserType), entity.Type.ToString())
            };
            return o;
        }
    }
}