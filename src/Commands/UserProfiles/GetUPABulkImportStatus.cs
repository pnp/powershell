using System.Management.Automation;
using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using System;
using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPUPABulkImportStatus")]
    [OutputType(typeof(ImportProfilePropertiesJobInfo))]
    public class GetUPABulkImportStatus : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public Guid JobId;

        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public SwitchParameter IncludeErrorDetails;

        protected override void ExecuteCmdlet()
        {
            var o365 = new Office365Tenant(AdminContext);

            if (ParameterSpecified(nameof(JobId)))
            {
                var job = o365.GetImportProfilePropertyJob(JobId);
                AdminContext.Load(job);
                AdminContext.ExecuteQueryRetry();

                GetErrorInfo(job);
                WriteObject(job, true);
            }
            else
            {
                ImportProfilePropertiesJobStatusCollection jobs = o365.GetImportProfilePropertyJobs();
                AdminContext.Load(jobs);
                AdminContext.ExecuteQueryRetry();
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
                var webUrl = Web.GetWebUrlFromPageUrl(AdminContext, job.LogFolderUri);
                AdminContext.ExecuteQueryRetry();
                string relativePath = job.LogFolderUri.Replace(webUrl.Value, "");
                var webCtx = AdminContext.Clone(webUrl.Value);
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