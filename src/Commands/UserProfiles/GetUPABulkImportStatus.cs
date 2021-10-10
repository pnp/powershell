using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using System;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPUPABulkImportStatus")]
    public class GetUPABulkImportStatus : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public Guid JobId;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public SwitchParameter IncludeErrorDetails;

        protected override void ExecuteCmdlet()
        {
            var o365 = new Office365Tenant(ClientContext);

            if (ParameterSpecified(nameof(JobId)))
            {
                var job = o365.GetImportProfilePropertyJob(JobId);
                ClientContext.Load(job);
                ClientContext.ExecuteQueryRetry();

                GetErrorInfo(job);
                WriteObject(job, true);
            }
            else
            {
                ImportProfilePropertiesJobStatusCollection jobs = o365.GetImportProfilePropertyJobs();
                ClientContext.Load(jobs);
                ClientContext.ExecuteQueryRetry();
                foreach (var job in jobs)
                {
                    GetErrorInfo(job);
                }
                WriteObject(jobs, true);
            }
        }

        private void GetErrorInfo(ImportProfilePropertiesJobInfo job)
        {
            if (job.Error != ImportProfilePropertiesJobError.NoError && IncludeErrorDetails == true)
            {
                var webUrl = Web.GetWebUrlFromPageUrl(ClientContext, job.LogFolderUri);
                ClientContext.ExecuteQueryRetry();
                string relativePath = job.LogFolderUri.Replace(webUrl.Value, "");
                var webCtx = ClientContext.Clone(webUrl.Value);
                if (webCtx.Web.DoesFolderExists(relativePath))
                {
                    var folder = webCtx.Web.GetFolderByServerRelativeUrl(relativePath);
                    var files = folder.Files;
                    webCtx.Load(folder);
                    webCtx.Load(files);
                    webCtx.ExecuteQueryRetry();
                    string message = string.Empty;
                    foreach (var logFile in files)
                        message += "\r\n" + webCtx.Web.GetFileAsString(logFile.ServerRelativeUrl);
                    job.ErrorMessage = message;
                }
            }
        }
    }
}