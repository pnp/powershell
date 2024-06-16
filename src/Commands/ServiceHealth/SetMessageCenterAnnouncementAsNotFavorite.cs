using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Set, "PnPMessageCenterAnnouncementAsNotFavorite")]
    [RequiredMinimalApiPermissions("ServiceMessageViewpoint.Write")]
    public class SetMessageCenterAnnouncementAsNotFavorite : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public string Identity;
        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsNotfavoriteById(this, Identity, Connection, AccessToken), true);
            }
            else
            {
                // Retrieve all message center announcements
                var messageCenterAnnouncements = ServiceHealthUtility.GetServiceUpdateMessages(this, Connection, AccessToken);

                // Create an array of the Ids of all message center announcements
                var messageCenterAnnouncementIds = messageCenterAnnouncements.Select(item => item.Id).ToArray();

                // Mark all message center announcements as not favorites
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsNotfavoriteById(this, messageCenterAnnouncementIds, Connection, AccessToken), true);
            }
        }        
    }
}