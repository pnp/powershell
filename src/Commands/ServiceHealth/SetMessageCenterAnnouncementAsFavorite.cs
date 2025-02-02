using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Set, "PnPMessageCenterAnnouncementAsFavorite")]
    [RequiredApiDelegatedPermissions("graph/ServiceMessageViewpoint.Write")]
    public class SetMessageCenterAnnouncementAsFavorite : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string[] Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsFavoriteById(GraphRequestHelper, Identity), true);
            }
            else
            {
                // Retrieve all message center announcements
                var messageCenterAnnouncements = ServiceHealthUtility.GetServiceUpdateMessages(GraphRequestHelper);

                // Create an array of the Ids of all message center announcements
                var messageCenterAnnouncementIds = messageCenterAnnouncements.Select(item => item.Id).ToArray();

                // Mark all message center announcements as favorite
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsFavoriteById(GraphRequestHelper, messageCenterAnnouncementIds), true);
            }
        }
    }
}