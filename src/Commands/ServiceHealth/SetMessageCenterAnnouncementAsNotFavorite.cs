using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Set, "PnPMessageCenterAnnouncementAsNotFavorite")]
    [RequiredApiDelegatedPermissions("graph/ServiceMessageViewpoint.Write")]
    public class SetMessageCenterAnnouncementAsNotFavorite : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;
        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsNotfavoriteById(RequestHelper, Identity), true);
            }
            else
            {
                // Retrieve all message center announcements
                var messageCenterAnnouncements = ServiceHealthUtility.GetServiceUpdateMessages(RequestHelper);

                // Create an array of the Ids of all message center announcements
                var messageCenterAnnouncementIds = messageCenterAnnouncements.Select(item => item.Id).ToArray();

                // Mark all message center announcements as not favorites
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsNotfavoriteById(RequestHelper, messageCenterAnnouncementIds), true);
            }
        }
    }
}