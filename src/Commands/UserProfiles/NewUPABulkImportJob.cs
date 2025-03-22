using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using PnP.Framework.Utilities;
using System.Threading;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.New, "PnPUPABulkImportJob", DefaultParameterSetName = ParameterSet_UPLOADFILE)]
    [OutputType(typeof(ImportProfilePropertiesJobInfo))]
    public class NewUPABulkImportJob : PnPSharePointOnlineAdminCmdlet
    {
        private const string ParameterSet_UPLOADFILE = "Submit up a new user profile bulk import job from local file";
        private const string ParameterSet_URL = "Submit up a new user profile bulk import job from url";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_UPLOADFILE)]
        public string Folder;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_UPLOADFILE)]
        public string Path = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_URL)]
        public string Url = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_URL)]
        public Hashtable UserProfilePropertyMapping;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_URL)]
        public string IdProperty;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_URL)]
        public ImportProfilePropertiesUserIdType IdType = ImportProfilePropertiesUserIdType.Email;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_URL)]
        public SwitchParameter Wait; 

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_URL)]
        public SwitchParameter WhatIf;               

        protected override void ExecuteCmdlet()
        {
            if (string.IsNullOrWhiteSpace(IdProperty))
            {
                throw new InvalidEnumArgumentException(@"IdProperty cannot be empty");
            }

            switch (ParameterSetName)
            {
                case ParameterSet_UPLOADFILE:
                    if (string.IsNullOrWhiteSpace(Path))
                    {
                        throw new InvalidEnumArgumentException(@"Path cannot be empty");
                    }

                    LogDebug($"Going to use mapping file to upload from {Path}");

                    var webCtx = AdminContext.Clone(Connection.Url);
                    var web = webCtx.Web;
                    var webServerRelativeUrl = web.EnsureProperty(w => w.ServerRelativeUrl);
                    if (!Folder.ToLower().StartsWith(webServerRelativeUrl))
                    {
                        Folder = UrlUtility.Combine(webServerRelativeUrl, Folder);
                    }
                    if (!web.DoesFolderExists(Folder))
                    {
                        throw new InvalidOperationException($"Folder {Folder} does not exist");
                    }
                    var folder = web.GetFolderByServerRelativeUrl(Folder);

                    var fileName = System.IO.Path.GetFileName(Path);

                    File file = null;
                    if(!ParameterSpecified(nameof(WhatIf)))
                    {
                        LogDebug($"Uploading file from {Path} to {fileName}");
                        file = folder.UploadFile(fileName, Path, true);
                    }
                    else
                    {
                        LogDebug($"Skipping uploading file from {Path} to {fileName} due to {nameof(WhatIf)} parameter being specified");
                    }
                    
                    Url = new Uri(webCtx.Url).GetLeftPart(UriPartial.Authority) + file?.ServerRelativeUrl;
                    break;
                case ParameterSet_URL:
                    if (string.IsNullOrWhiteSpace(Url))
                    {
                        throw new InvalidEnumArgumentException(@"Url cannot be empty");
                    }
                    LogDebug($"Will instruct SharePoint Online to use mapping file located at {Url}");
                    break;
            }

            var o365 = new Office365Tenant(AdminContext);
            var propDictionary = UserProfilePropertyMapping.Cast<DictionaryEntry>().ToDictionary(kvp => (string)kvp.Key, kvp => (string)kvp.Value);

            Guid? jobId = null;
            if (!ParameterSpecified(nameof(WhatIf)))
            {
                LogDebug($"Instructing SharePoint Online to queue user profile file located at {Url}");
                var id = o365.QueueImportProfileProperties(IdType, IdProperty, propDictionary, Url);
                AdminContext.ExecuteQueryRetry();

                if (id.Value != Guid.Empty)
                {
                    jobId = id.Value;
                }
            }
            else
            {
                LogDebug($"Skipping instructing SharePoint Online to queue user profile file located at {Url} due to {nameof(WhatIf)} parameter being specified");
                return;
            }

            // For some reason it sometimes does not always properly return the JobId while the job did start. Show this in the output.
            if(!jobId.HasValue || jobId.Value == Guid.Empty)
            {
                LogWarning("The execution of the synchronization job did not return a job Id but seems to have started successfully. Use Get-PnPUPABulkImportStatus to check for the current status.");
                return;
            }

            var job = o365.GetImportProfilePropertyJob(jobId.Value);
            AdminContext.Load(job);
            AdminContext.ExecuteQueryRetry();

            LogDebug($"Job initiated with Id {job.JobId} and status {job.State} for file {job.SourceUri}");

            // Check if we should wait with finalzing this cmdlet execution until the user profile import operation has completed
            if(Wait.ToBool())
            {
                // Go into a loop to wait for the import to be successful or erroneous
                ImportProfilePropertiesJobInfo jobStatus;
                short waitBetweenChecks = 30; // In seconds
                do
                {
                    // Wait before requesting its current state again to avoid running into throttling
                    LogDebug($"Waiting for {waitBetweenChecks} seconds before querying for the status of job Id {job.JobId}");
                    Thread.Sleep((int)System.TimeSpan.FromSeconds(waitBetweenChecks).TotalMilliseconds);                    

                    // Request the current status of the import job
                    jobStatus = o365.GetImportProfilePropertyJob(job.JobId);
                    AdminContext.Load(jobStatus);
                    AdminContext.ExecuteQueryRetry();

                    LogDebug($"Current status of job {job.JobId}: {jobStatus.State}");
                }
                while (jobStatus.State != ImportProfilePropertiesJobState.Succeeded && jobStatus.State != ImportProfilePropertiesJobState.Error);

                // Import job either completed or failed
                job = jobStatus;
            }

            WriteObject(job);
        }
    }
}