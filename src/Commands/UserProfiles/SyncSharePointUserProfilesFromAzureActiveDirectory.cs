using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model.SharePoint.SharePointUserProfileSync;
using PnP.PowerShell.Commands.Utilities.EntraID;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsData.Sync, "PnPSharePointUserProfilesFromAzureActiveDirectory")]
    [OutputType(typeof(SharePointUserProfileSyncStatus))]
    public class SyncSharePointUserProfilesFromAzureActiveDirectory : PnPSharePointCmdlet
    {
        [Parameter(Mandatory = false)]
        public List<Model.AzureAD.User> Users;

        [Parameter(Mandatory = false)]
        public string Folder = "Shared Documents";

        [Parameter(Mandatory = true)]
        public Hashtable UserProfilePropertyMapping;

        [Parameter(Mandatory = false)]
        public SwitchParameter WhatIf;

        [Parameter(Mandatory = false)]
        public ImportProfilePropertiesUserIdType IdType = ImportProfilePropertiesUserIdType.PrincipalName;

        [Parameter(Mandatory = false)]
        public SwitchParameter Wait;

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrWhiteSpace(Folder))
            {
                throw new PSArgumentNullException(nameof(Folder), "Folder cannot be empty");
            }

            var aadUsers = new List<PnP.PowerShell.Commands.Model.AzureAD.User>();
            if (ParameterSpecified(nameof(Users)))
            {
                // Ensure users have been provided
                if(Users == null)
                {
                    throw new PSArgumentNullException(nameof(Users), "Provided Users collection cannot be null");
                }
                if(Users.Count == 0)
                {
                    WriteVerbose("No users have been provided");
                    return;
                }


                // Users to sync have been provided
                WriteVerbose($"Using provided user collection containing {Users.Count} user{(Users.Count != 1 ? "s": "")}");

                aadUsers = Users;
            }
            else
            {
                // No users to sync have been provided, retrieve all users
                // Construct an array with all the Entra ID properties that need to be fetched from the users to be able to make the mapping
                var allAadPropertiesList = new List<string>();
                foreach (DictionaryEntry userProfilePropertyMappingEntry in UserProfilePropertyMapping)
                {
                    if (userProfilePropertyMappingEntry.Value != null && !allAadPropertiesList.Contains(userProfilePropertyMappingEntry.Value.ToString()))
                    {
                        allAadPropertiesList.Add(userProfilePropertyMappingEntry.Value.ToString());
                    }
                }

                WriteVerbose("Retrieving users from Entra ID");

                // Retrieve all the users from Entra ID
                aadUsers = UsersUtility.ListUsers(GraphAccessToken, null, null, allAadPropertiesList.ToArray(), endIndex: null);

                WriteVerbose($"{aadUsers.Count} user{(aadUsers.Count != 1 ? "s have" : " has")} been retrieved from Entra ID");

                if (aadUsers.Count == 0)
                {
                    throw new PSInvalidOperationException($"No Entra ID users found to process");
                }
            }

            // Create a ClientContext connecting to the site specified through the Connect-PnPOnline cmdlet instead of the current potential Admin ClientContext.
            var nonAdminClientContext = ClientContext.Clone(Connection.Url);

            // Perform the mapping and execute the sync operation
            WriteVerbose($"Creating mapping file{(WhatIf.ToBool() ? " and" : ",")} uploading it to SharePoint Online to folder '{Folder}'{(WhatIf.ToBool() ? "" : " and executing sync job")}");
            var job = Utilities.SharePointUserProfileSync.SyncFromAzureActiveDirectory(nonAdminClientContext, aadUsers, IdType, UserProfilePropertyMapping, Folder, ParameterSpecified(nameof(WhatIf))).GetAwaiter().GetResult();

            // Ensure a sync job has been created
            if(job == null)
            {
                throw new PSInvalidOperationException($"Failed to create sync job. Ensure you're providing users to sync and that the mapping is correct.");
            }

            WriteVerbose($"Job initiated with {(job.JobId.HasValue ? $"Id {job.JobId} and ": "")}status {job.State} for file {job.SourceUri}");

            // Check if we should wait with finalzing this cmdlet execution until the user profile import operation has completed
            if (Wait.ToBool() && !WhatIf.ToBool())
            {
                // Go into a loop to wait for the import to be successful or erroneous
                var o365 = new Office365Tenant(ClientContext);

                ImportProfilePropertiesJobInfo jobStatus;
                do
                {
                    // Wait for 30 seconds before requesting its current state again to avoid running into throttling
                    Thread.Sleep((int)System.TimeSpan.FromSeconds(30).TotalMilliseconds);

                    // Request the current status of the import job
                    jobStatus = o365.GetImportProfilePropertyJob(job.JobId.Value);
                    ClientContext.Load(jobStatus);
                    ClientContext.ExecuteQueryRetry();

                    WriteVerbose($"Current status of job {job.JobId.Value}: {jobStatus.State}");
                }
                while (jobStatus.State != ImportProfilePropertiesJobState.Succeeded && jobStatus.State != ImportProfilePropertiesJobState.Error);

                // Import job either completed or failed
                job = SharePointUserProfileSyncStatus.ParseFromImportProfilePropertiesJobInfo(jobStatus);
            }

            // Write the sync job details
            WriteObject(job);
        }
    }
}