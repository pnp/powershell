using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.Framework.Utilities;
using System.Threading.Tasks;
using System.Reflection;
using PnP.PowerShell.Commands.Model.SharePoint.SharePointUserProfileSync;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Contains utilities to allow synchronization of user profiles from various sources to the SharePoint Online user profile
    /// </summary>
    public static class SharePointUserProfileSync
    {
        /// <summary>
        /// Syncs from Entra ID to SharePoint Online user profiles
        /// </summary>
        /// <param name="clientContext">A ClientContext which can be used to interact with SharePoint Online</param>
        /// <param name="users">Entra ID User objects that need to be synced</param>
        /// <param name="userProfilePropertyMappings">Hashtable with the mapping from the Entra ID property (the value) to the SharePoint Online User Profile Property (the key)</param>
        /// <param name="idType">Type of identifier to map the user on to synchronize its user profile of (CloudId, PrincipalName, Email)</param>
        /// <param name="sharePointFolder">Location in the currently connected to site where to upload the JSON file to with instructions to update the user profiles</param>
        /// <param name="onlyCreateAndUploadMappingsFile">Boolean indicating if only the mappings file should be created and uploaded to SharePoint Online (true) or if the import job on that file should also be invoked (false)</param>
        /// <returns>Information on the status of the import job that has been created because of this action</returns>
        public static async Task<SharePointUserProfileSyncStatus> SyncFromAzureActiveDirectory(ClientContext clientContext, IEnumerable<PnP.PowerShell.Commands.Model.AzureAD.User> users, ImportProfilePropertiesUserIdType idType, Hashtable userProfilePropertyMappings, string sharePointFolder, bool onlyCreateAndUploadMappingsFile = false)
        {
            var webServerRelativeUrl = clientContext.Web.EnsureProperty(w => w.ServerRelativeUrl);
            if (!sharePointFolder.ToLower().StartsWith(webServerRelativeUrl))
            {
                sharePointFolder = UrlUtility.Combine(webServerRelativeUrl, sharePointFolder);
            }
            if (!clientContext.Web.DoesFolderExists(sharePointFolder))
            {
                throw new InvalidOperationException($"Folder {sharePointFolder} to upload the user profile update file to does not exist on SharePoint Online in the site {clientContext.Url}");
            }
            
            var folder = clientContext.Web.GetFolderByServerRelativeUrl(sharePointFolder);

            var bulkUpdateBuilder = new StringBuilder();
            var userUpdateBuilder = new StringBuilder();
            foreach(var user in users)
            {  
                foreach (DictionaryEntry userProfilePropertyMapping in userProfilePropertyMappings)
                {
                    if (userProfilePropertyMapping.Key != null && userProfilePropertyMapping.Value != null)
                    {
                        // Check if the property is a property directly on the user object
                        var aadUserProperty = user.GetType().GetProperty(userProfilePropertyMapping.Value.ToString(), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                        if (aadUserProperty != null)
                        {
                            // Construct an entry with the SharePoint Online User Profile property name and the value it should be set to coming from a property on the User object
                            if(aadUserProperty.PropertyType == typeof(IEnumerable<string>))
                            {
                                // AAD User property is an array, join all entries with a comma and add the combined string to the mapping output
                                userUpdateBuilder.AppendFormat(@"""{0}"":""{1}"",", userProfilePropertyMapping.Key, string.Join(",", ((IEnumerable)aadUserProperty.GetValue(user)).Cast<string>().ToArray()));
                            }
                            else
                            {
                                // AAD User property is a string, add its value to the mapping output
                                userUpdateBuilder.AppendFormat(@"""{0}"":""{1}"",", userProfilePropertyMapping.Key, aadUserProperty.GetValue(user));
                            }
                        }
                        else if (user.AdditionalProperties != null && user.AdditionalProperties.TryGetValue(userProfilePropertyMapping.Value.ToString(), out object userProfilePropertyMappingValue))
                        {
                            // Construct an entry with the SharePoint Online User Profile property name and the value it should be set to coming from a property on the AdditionalProperties dictionary on the User object
                            userUpdateBuilder.AppendFormat(@"""{0}"":""{1}"",", userProfilePropertyMapping.Key, userProfilePropertyMappingValue != null ? userProfilePropertyMappingValue.ToString() : string.Empty);
                        }
                    }                    
                }

                // If there are properties to update for this user, add the IdName property for this user and the fields to update to the mapping output
                if(userUpdateBuilder.Length > 0)
                {
                    bulkUpdateBuilder.Append(@"{""IdName"":""");

                    // Map the proper IdType property based on the IdType that we need to use to identify the user by
                    switch(idType)
                    {
                        case ImportProfilePropertiesUserIdType.CloudId: 
                            bulkUpdateBuilder.Append(user.Id);
                            break;

                        case ImportProfilePropertiesUserIdType.Email: 
                            bulkUpdateBuilder.Append(user.Mail);
                            break;

                        case ImportProfilePropertiesUserIdType.PrincipalName:
                            bulkUpdateBuilder.Append(user.UserPrincipalName);
                            break;
                    }
                    
                    bulkUpdateBuilder.Append(@""",");
                    bulkUpdateBuilder.Append(userUpdateBuilder.ToString().TrimEnd(',')).Replace(@"\", @"\\");
                    bulkUpdateBuilder.Append("},");

                    userUpdateBuilder.Clear();
                }
            }

            // Check if there's anything to update
            if(bulkUpdateBuilder.Length == 0)
            {
                return null;
            }

            // Construct the entire JSON message with the user profiles and properties to update
            var json = @"{ ""value"": [" + bulkUpdateBuilder.ToString().TrimEnd(',') + "] }";

            // Define the filename to save the file under on SharePoint Online
            var fileName = $"userprofilesyncdata-{DateTime.Now.ToString("yyyyMMddHHmmss")}-{Guid.NewGuid().ToString().Replace("-", "")}.json";

            // Upload the JSON to SharePoint Online
            File file = null;
            using (var stream = new System.IO.MemoryStream())
            {
                using (var writer = new System.IO.StreamWriter(stream, Encoding.UTF8))
                {
                    await writer.WriteAsync(json);
                    writer.Flush();
                    stream.Position = 0;

                    file = folder.UploadFile(fileName, stream, true);
                }
            }

            // Check if we should kick off the process to import the file
            if (onlyCreateAndUploadMappingsFile)
            {
                return new SharePointUserProfileSyncStatus
                {
                    SourceUri = new Uri(clientContext.Url).GetLeftPart(UriPartial.Authority) + file.ServerRelativeUrl,
                    State = Enums.SharePointUserProfileImportProfilePropertiesJobState.WontStart
                };
            }

            // Instruct SharePoint Online to process the JSON file
            var o365 = new Office365Tenant(clientContext);
            var propDictionary = userProfilePropertyMappings.Cast<DictionaryEntry>().ToDictionary(kvp => (string)kvp.Key, kvp => (string)kvp.Key);
            var url = new Uri(clientContext.Url).GetLeftPart(UriPartial.Authority) + file.ServerRelativeUrl;
            var id = o365.QueueImportProfileProperties(idType, "IdName", propDictionary, url);
            clientContext.ExecuteQueryRetry();

            // Retrieve the import json details
            var job = o365.GetImportProfilePropertyJob(id.Value);
            clientContext.Load(job);
            clientContext.ExecuteQueryRetry();

            // Map the CSOM result object to our own entity
            var sharePointUserProfileSyncStatus = SharePointUserProfileSyncStatus.ParseFromImportProfilePropertiesJobInfo(job);
            return sharePointUserProfileSyncStatus;
        }
    }
}