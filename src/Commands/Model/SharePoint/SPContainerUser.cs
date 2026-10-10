using PnP.PowerShell.Commands.Enums;

namespace PnP.PowerShell.Commands.Model.SharePoint
{
    /// <summary>
    /// A user with a role on a SharePoint Embedded container
    /// </summary>
    public class SPContainerUser
    {
        /// <summary>
        /// Id of the container
        /// </summary>
        public string ContainerId { get; private set; }

        /// <summary>
        /// Login name of the user, as returned by SharePoint Online
        /// </summary>
        public string LoginName { get; private set; }

        /// <summary>
        /// Role of the user on the container
        /// </summary>
        public ContainerRole Role { get; private set; }

        internal SPContainerUser(string containerId, string loginName, ContainerRole role)
        {
            ContainerId = containerId;
            LoginName = loginName;
            Role = role;
        }
    }
}
