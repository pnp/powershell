using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities;
using System.Linq;

namespace PnP.PowerShell.Commands.ServiceHealth
{
    [Cmdlet(VerbsCommon.Set, "PnPMessageCenterAnnouncementAsFavorite", DefaultParameterSetName = ParameterSet_ALL)]
    [RequiredMinimalApiPermissions("ServiceMessageViewpoint.Write")]
    public class SetMessageCenterAnnouncementAsFavorite : PnPGraphCmdlet
    {
        private const string ParameterSet_ALL = "All";
        private const string ParameterSet_SINGLE = "Single";
        private const string ParameterSet_MULTIPLE = "Multiple";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_SINGLE)]
        public string Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_MULTIPLE)]
        public string[] Identities;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsFavoriteByIdAsync(Identity, HttpClient, AccessToken).GetAwaiter().GetResult(), true);
            }
            else if (ParameterSpecified(nameof(Identities)))
            {
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsFavoriteByIdAsync(Identities, HttpClient, AccessToken).GetAwaiter().GetResult(), true);
            }
            else
            {
                // Retrieve all message center announcements
                var messageCenterAnnouncements = ServiceHealthUtility.GetServiceUpdateMessagesAsync(HttpClient, AccessToken).GetAwaiter().GetResult();

                // Create an array of the Ids of all message center announcements
                var messageCenterAnnouncementIds = messageCenterAnnouncements.Select(item => item.Id).ToArray();

                // Mark all message center announcements as favorite
                WriteObject(ServiceHealthUtility.SetServiceUpdateMessageAsFavoriteByIdAsync(messageCenterAnnouncementIds, HttpClient, AccessToken).GetAwaiter().GetResult(), true);
            }
        }        
    }
}