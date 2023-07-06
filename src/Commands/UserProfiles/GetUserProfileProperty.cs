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
            // All the basic profile profile properties that only exist on the PersonProperties object
            var basicProperties = new string[] { "AccountName", "DirectReports", "DisplayName", "Email", "ExtendedManagers", "ExtendedReports", "IsFollowed", "LatestPost", "Peers", "PersonalSiteHostUrl", "PersonalUrl", "PictureUrl", "Title", "UserUrl" };

            var peopleManager = new PeopleManager(AdminContext);

            // Loop through each of the requested users
            foreach (var acc in Account)
            {
                var currentAccount = acc;
                var result = Tenant.EncodeClaim(currentAccount);
                AdminContext.ExecuteQueryRetry();
                currentAccount = result.Value;

                SortedDictionary<string, object> upsDictionary = new();

                // Check if specific user profile properties have been requested
                if (ParameterSpecified(nameof(Properties)) && Properties != null && Properties.Length > 0 && Properties.All(p => !basicProperties.Contains(p)))
                {
                    // Specific user profile properties have been requested and none of them are basic user profile properties, return only those properties that have been requested
                    UserProfilePropertiesForUser userProfilePropertiesForUser = new UserProfilePropertiesForUser(AdminContext, currentAccount, Properties);
                    var userRequestedProperties = peopleManager.GetUserProfilePropertiesFor(userProfilePropertiesForUser);
                    AdminContext.Load(userProfilePropertiesForUser);
                    AdminContext.ExecuteQueryRetry();

                    // Add all the requested extended user profile properties to the output
                    for (var i = 0; i < userRequestedProperties.Count(); i++)
                    {
                        object propertyValue = userRequestedProperties.ElementAt(i);
                        upsDictionary.Add(Properties[i], propertyValue);
                    }
                }
                else
                {
                    // No specific user profile properties have been requested or there were basic user profile properties amongst them
                    var userProfileProperties = peopleManager.GetPropertiesFor(currentAccount);
                    AdminContext.Load(userProfileProperties);
                    AdminContext.ExecuteQueryRetry();

                    // Check if we only need to output specific properties or all of them
                    if (ParameterSpecified(nameof(Properties)) && Properties != null && Properties.Length > 0)
                    {
                        // Check if any of the base user profile properties have been requested and if so, add them to the output as well
                        if (Properties.Contains("AccountName")) upsDictionary.Add("AccountName", userProfileProperties.AccountName);
                        if (Properties.Contains("DirectReports")) upsDictionary.Add("DirectReports", userProfileProperties.DirectReports);
                        if (Properties.Contains("DisplayName")) upsDictionary.Add("DisplayName", userProfileProperties.DisplayName);
                        if (Properties.Contains("Email")) upsDictionary.Add("Email", userProfileProperties.Email);
                        if (Properties.Contains("ExtendedManagers")) upsDictionary.Add("ExtendedManagers", userProfileProperties.ExtendedManagers);
                        if (Properties.Contains("ExtendedReports")) upsDictionary.Add("ExtendedReports", userProfileProperties.ExtendedReports);
                        if (Properties.Contains("IsFollowed")) upsDictionary.Add("IsFollowed", userProfileProperties.IsFollowed);
                        if (Properties.Contains("LatestPost")) upsDictionary.Add("LatestPost", userProfileProperties.LatestPost);
                        if (Properties.Contains("Peers")) upsDictionary.Add("Peers", userProfileProperties.Peers);
                        if (Properties.Contains("PersonalSiteHostUrl")) upsDictionary.Add("PersonalSiteHostUrl", userProfileProperties.PersonalSiteHostUrl);
                        if (Properties.Contains("PersonalUrl")) upsDictionary.Add("PersonalUrl", userProfileProperties.PersonalUrl);
                        if (Properties.Contains("PictureUrl")) upsDictionary.Add("PictureUrl", userProfileProperties.PictureUrl);
                        if (Properties.Contains("Title")) upsDictionary.Add("Title", userProfileProperties.Title);
                        if (Properties.Contains("UserUrl")) upsDictionary.Add("UserUrl", userProfileProperties.UserUrl);

                        // Add the extended user profile properties to the output which have been specified in Properties
                        if (userProfileProperties.UserProfileProperties != null && userProfileProperties.UserProfileProperties.Count > 0)
                        {
                            for (var i = 0; i < userProfileProperties.UserProfileProperties.Count; i++)
                            {
                                var element = userProfileProperties.UserProfileProperties.ElementAt(i);

                                // Check if this property should be included in the output, if not continue with the next property
                                if(!Properties.Contains(element.Key)) continue;

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
                    }
                    else
                    {
                        // Add all of the basic user profile properties to the output
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

                        // Add all the extended user profile properties to the output
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
                    }
                }

                // Write the collected properties to the output stream
                WriteObject(upsDictionary, true);
            }
        }
    }
}