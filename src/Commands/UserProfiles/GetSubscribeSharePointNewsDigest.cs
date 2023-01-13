using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using System.Linq;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using System;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPSubscribeSharePointNewsDigest")]
    [OutputType(typeof(SubscribeSharePointNewsDigestStatus))]
    [Obsolete("The implementation behind this feature has changed in SharePoint Online making it impossible at the moment to call this using PnP PowerShell. This cmdlet therefore no longer works and will be removed in a future version.")]
    public class GetSubscribeSharePointNewsDigest : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Account;

        protected override void ExecuteCmdlet()
        {
            WriteVerbose($"Encoding user account {Account}");

            var peopleManager = new PeopleManager(ClientContext);
            var result = Tenant.EncodeClaim(Account);
            ClientContext.ExecuteQueryRetry();

            WriteVerbose($"Retrieving user profile for {result.Value}");

            var properties = peopleManager.GetPropertiesFor(result.Value);
            ClientContext.Load(properties, p => p.PersonalUrl);
            ClientContext.ExecuteQueryRetry();

            WriteVerbose($"Connecting to OneDrive for Business site at {properties.PersonalUrl}");

            var oneDriveContext = Connection.CloneContext(properties.PersonalUrl);

            WriteVerbose("Retrieving notificationSubscriptionHiddenList list");

            // Retrieve all lists as the list name starts with 'notificationSubscriptionHiddenList' and ends with an unknown GUID
            oneDriveContext.Load(oneDriveContext.Web.Lists, l => l.Include(li => li.Title));
            oneDriveContext.ExecuteQueryRetry();
            
            // Check if we have a list of which the name starts with 'notificationSubscriptionHiddenList'
            var notificationsList = oneDriveContext.Web.Lists.First(l => l.Title.StartsWith("notificationSubscriptionHiddenList"));

            if(notificationsList == null)
            {
                throw new System.Exception("Unable to locate notificationSubscriptionHiddenList list");
            }

            WriteVerbose($"Retrieving list items from {notificationsList.Title}");

            CamlQuery query = new CamlQuery
            {
                ViewXml = $"<View><Query><Where><And><Eq><FieldRef Name='SubscriptionId'/><Value Type='Text'>email_unsubscribe_AutoNewsDigest</Value></Eq><Eq><FieldRef Name='NotificationScenarios' /><Value Type='Text'>AutoNewsDigest</Value></Eq></And></Where></Query></View>"
            };

            var listItems = notificationsList.GetItems(query);
            oneDriveContext.Load(listItems);
            oneDriveContext.ExecuteQueryRetry();

            WriteVerbose($"{listItems.Count} item{(listItems.Count != 1 ? "s" : "")} returned");

            var record = new SubscribeSharePointNewsDigestStatus(Account, enabled: listItems.Count == 0);


            WriteObject(record);
        }
    }
}
