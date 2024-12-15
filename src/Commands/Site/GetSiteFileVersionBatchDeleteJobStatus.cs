using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteFileVersionBatchDeleteJobStatus")]
    [OutputType(typeof(FileVersionBatchDeleteJobStatus))]
    public class GetSiteFileVersionBatchDeleteJobStatus : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var site = ClientContext.Site;
            ClientContext.Load(site, s => s.Url);
            var ret = site.GetProgressForDeleteFileVersions();
            ClientContext.ExecuteQueryRetry();

            var progress = JsonSerializer.Deserialize<FileVersionBatchDeleteJobStatus>(ret.Value);
            progress.Url = site.Url;

            if (!string.Equals(progress.BatchDeleteMode, FileVersionBatchDeleteMode.DeleteOlderThanDays.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                progress.DeleteOlderThan = string.Empty;
            }

            if (!string.Equals(progress.BatchDeleteMode, FileVersionBatchDeleteMode.CountLimits.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                progress.MajorVersionLimit = string.Empty;
                progress.MajorWithMinorVersionsLimit = string.Empty;
            }

            if (string.Equals(progress.LastProcessTimeInUTC, DateTime.MinValue.ToString()))
            {
                progress.LastProcessTimeInUTC = string.Empty;
            }

            if (string.Equals(progress.CompleteTimeInUTC, DateTime.MinValue.ToString()))
            {
                progress.CompleteTimeInUTC = string.Empty;
            }

            WriteObject(progress);
        }
    }
}
