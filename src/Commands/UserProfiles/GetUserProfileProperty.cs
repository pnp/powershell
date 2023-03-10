using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;

using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPUserProfileProperty")]
    [OutputType(typeof(SortedDictionary<string, object>))]
    public class GetUserProfileProperty : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string[] Account;

        [Parameter(Mandatory = false)]
        public string[] Properties;

        protected override void ExecuteCmdlet()
        {
            var peopleManager = new PeopleManager(ClientContext);

            foreach (var acc in Account)
            {
                var currentAccount = acc;
                var result = Tenant.EncodeClaim(currentAccount);
                ClientContext.ExecuteQueryRetry();
                currentAccount = result.Value;

                SortedDictionary<string, object> upsDictionary = new();

                if (ParameterSpecified(nameof(Properties)) && Properties != null && Properties.Length > 0)
                {
                    UserProfilePropertiesForUser userProfilePropertiesForUser = new UserProfilePropertiesForUser(ClientContext, currentAccount, Properties);
                    var userRequestedProperties = peopleManager.GetUserProfilePropertiesFor(userProfilePropertiesForUser);
                    ClientContext.Load(userProfilePropertiesForUser);
                    ClientContext.ExecuteQueryRetry();

                    for (var i = 0; i < userRequestedProperties.Count(); i++)
                    {
                        object propertyValue = userRequestedProperties.ElementAt(i);
                        upsDictionary.Add(Properties[i], propertyValue);
                    }

                    WriteObject(upsDictionary, true);

                }
                else
                {
                    var userProfileProperties = peopleManager.GetPropertiesFor(currentAccount);
                    ClientContext.Load(userProfileProperties);
                    ClientContext.ExecuteQueryRetry();

                    upsDictionary.Add("AccountName", userProfileProperties.AccountName);
                    upsDictionary.Add("DirectReports", userProfileProperties.DirectReports);
                    upsDictionary.Add("DisplayName", userProfileProperties.DisplayName);
                    upsDictionary.Add("Email", userProfileProperties.Email);
                    upsDictionary.Add("ExtendedManagers", userProfileProperties.ExtendedManagers);
                    upsDictionary.Add("ExtendedReports", userProfileProperties.ExtendedReports);
                    upsDictionary.Add("IsFollowed", userProfileProperties.IsFollowed);
                    upsDictionary.Add("LatestPost", userProfileProperties.LatestPost);
                    upsDictionary.Add("Peers", userProfileProperties.Peers);
                    upsDictionary.Add("PersonalSiteHostUrl", userProfileProperties.PersonalSiteHostUrl);
                    upsDictionary.Add("PersonalUrl", userProfileProperties.PersonalUrl);
                    upsDictionary.Add("PictureUrl", userProfileProperties.PictureUrl);
                    upsDictionary.Add("Title", userProfileProperties.Title);
                    upsDictionary.Add("UserUrl", userProfileProperties.UserUrl);

                    if (userProfileProperties.UserProfileProperties != null && userProfileProperties.UserProfileProperties.Count > 0)
                    {
                        for (var i = 0; i < userProfileProperties.UserProfileProperties.Count; i++)
                        {
                            var element = userProfileProperties.UserProfileProperties.ElementAt(i);
                            if (!upsDictionary.ContainsKey(element.Key))
                            {
                                upsDictionary.Add(element.Key, element.Value);
                            }
                            else
                            {
                                upsDictionary[element.Key] = element.Value;
                            }
                        }
                    }

                    WriteObject(upsDictionary, true);
                }
            }
        }
    }
}
