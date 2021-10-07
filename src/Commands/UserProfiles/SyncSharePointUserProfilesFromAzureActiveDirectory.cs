using System;
using System.Collections;
using System.Linq;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Collections.Generic;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsData.Sync, "PnPSharePointUserProfilesFromAzureActiveDirectory")]
    public class SyncSharePointUserProfilesFromAzureActiveDirectory : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public Array Users;

        [Parameter(Mandatory = false)]
        public string Folder = "Shared Documents";

        [Parameter(Mandatory = true)]
        public Hashtable UserProfilePropertyMapping;

        [Parameter(Mandatory = false)]
        public SwitchParameter WhatIf;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrWhiteSpace(Folder))
            {
                throw new PSArgumentNullException(nameof(Folder), "Folder cannot be empty");
            }

            var aadUsers = new List<PnP.PowerShell.Commands.Model.AzureAD.User>();
            if (ParameterSpecified(nameof(Users)))
            {
                // Users to sync have been provided
                if(Users == null)
                {
                    throw new PSArgumentNullException(nameof(Users), "Provided Users collection cannot be null");
                }

                // Loop through provided Azure Active Directory User objects
                foreach (PSObject user in Users)
                {
                    // Only accept and process PnP Framework User model entities
                    if (user.ImmediateBaseObject is PnP.Framework.Graph.Model.User aadUser)
                    {
                        aadUsers.Add(PnP.PowerShell.Commands.Model.AzureAD.User.CreateFrom(aadUser));
                    }
                }
                
                if(aadUsers.Count == 0)
                {
                    throw new PSArgumentException($"No valid Azure Active Directory users provided through {nameof(Users)} parameter", nameof(Users));
                }
            }
            else
            {
                // No users to sync have been provided, retrieve all users
                // Construct an array with all the Azure Active Directory properties that need to be fetched from the users to be able to make the mapping
                var allAadPropertiesList = new List<string>();
                foreach(DictionaryEntry userProfilePropertyMappingEntry in UserProfilePropertyMapping)
                {
                    if (userProfilePropertyMappingEntry.Value != null && !allAadPropertiesList.Contains(userProfilePropertyMappingEntry.Value.ToString()))
                    {
                        allAadPropertiesList.Add(userProfilePropertyMappingEntry.Value.ToString());
                    }
                }

                // Retrieve all the users from Azure Active Directory
                aadUsers = PnP.Framework.Graph.UsersUtility.ListUsers(GraphAccessToken, allAadPropertiesList.ToArray()).Select(u => PnP.PowerShell.Commands.Model.AzureAD.User.CreateFrom(u)).ToList();

                if(aadUsers.Count == 0)
                {
                    throw new PSInvalidOperationException($"No Azure Active Directory users found to process");
                }
            }

            // Create a ClientContext connecting to the site specified through the Connect-PnPOnline cmdlet instead of the current potential Admin ClientContext.
            var nonAdminClientContext = ClientContext.Clone(PnPConnection.Current.Url);

            // Perform the mapping and execute the sync operation
            var job = PnP.PowerShell.Commands.Utilities.SharePointUserProfileSync.SyncFromAzureActiveDirectory(nonAdminClientContext, aadUsers, UserProfilePropertyMapping, Folder, ParameterSpecified(nameof(WhatIf))).GetAwaiter().GetResult();

            // Write the sync job details
            WriteObject(job);
        }
    }
}