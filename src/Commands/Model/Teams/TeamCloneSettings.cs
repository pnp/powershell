using System.Text.Json.Serialization;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph;

namespace PnP.PowerShell.Commands.Model.Teams
{
    /// <summary>
    /// Contains instructions for creating a copy of an existing Teams team
    /// </summary>
    public class TeamCloneInformation
    {
        /// <summary>
        /// Display name to use for the copy
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description to use for the copy
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// E-mail address to use for the copy
        /// </summary>
        public string MailNickName { get; set; }

        /// <summary>
        /// Group visibility to apply to the copy
        /// </summary>
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public GroupVisibility Visibility { get; set; }

        /// <summary>
        /// Site classifiation to use for the copy
        /// </summary>
        public string Classification { get; set; }

        /// <summary>
        /// Array with parts to copy from the source to the target
        /// </summary>
        public ClonableTeamParts[] PartsToClone { get; set; }
    }
}