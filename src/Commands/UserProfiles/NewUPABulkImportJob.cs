﻿using System;
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
    public class NewUPABulkImportJob : PnPAdminCmdlet
    {
        private const string ParameterSet_UPLOADFILE = "Submit up a new user profile bulk import job from local file";
        private const string ParameterSet_URL = "Submit up a new user profile bulk import job from url";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ParameterSet_UPLOADFILE)]
        public string Folder;

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_UPLOADFILE)]
        public string Path = string.Empty;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_URL)]
        public string Url = string.Empty;

        [Parameter(Mandatory = true, Position = 2, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = ParameterSet_URL)]
        public Hashtable UserProfilePropertyMapping;

        [Parameter(Mandatory = true, Position = 3, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = ParameterSet_URL)]
        public string IdProperty;

        [Parameter(Mandatory = false, Position = 4, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = ParameterSet_URL)]
        public ImportProfilePropertiesUserIdType IdType = ImportProfilePropertiesUserIdType.Email;

        [Parameter(Mandatory = false, Position = 4, ParameterSetName = ParameterSet_UPLOADFILE)]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = ParameterSet_URL)]
        public SwitchParameter Wait;        

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

                    var webCtx = ClientContext.Clone(PnPConnection.Current.Url);
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
                    File file = folder.UploadFile(fileName, Path, true);
                    Url = new Uri(webCtx.Url).GetLeftPart(UriPartial.Authority) + file.ServerRelativeUrl;
                    break;
                case ParameterSet_URL:
                    if (string.IsNullOrWhiteSpace(Url))
                    {
                        throw new InvalidEnumArgumentException(@"Url cannot be empty");
                    }
                    break;
            }

            var o365 = new Office365Tenant(ClientContext);
            var propDictionary = UserProfilePropertyMapping.Cast<DictionaryEntry>().ToDictionary(kvp => (string)kvp.Key, kvp => (string)kvp.Value);
            var id = o365.QueueImportProfileProperties(IdType, IdProperty, propDictionary, Url);
            ClientContext.ExecuteQueryRetry();

            var job = o365.GetImportProfilePropertyJob(id.Value);
            ClientContext.Load(job);
            ClientContext.ExecuteQueryRetry();

            WriteVerbose($"Job initiated with Id {job.JobId} and status {job.State} for file {job.SourceUri}");

            // Check if we should wait with finalzing this cmdlet execution until the user profile import operation has completed
            if(Wait.ToBool())
            {
                // Go into a loop to wait for the import to be successful or erroneous
                ImportProfilePropertiesJobInfo jobStatus;
                do
                {
                    // Wait for 30 seconds before requesting its current state again to avoid running into throttling
                    Thread.Sleep((int)System.TimeSpan.FromSeconds(30).TotalMilliseconds);                    

                    // Request the current status of the import job
                    jobStatus = o365.GetImportProfilePropertyJob(job.JobId);
                    ClientContext.Load(jobStatus);
                    ClientContext.ExecuteQueryRetry();

                    WriteVerbose($"Current status of job {job.JobId}: {jobStatus.State}");
                }
                while (jobStatus.State != ImportProfilePropertiesJobState.Succeeded && jobStatus.State != ImportProfilePropertiesJobState.Error);

                // Import job either completed or failed
                job = jobStatus;
            }

            WriteObject(job);
        }
    }
}