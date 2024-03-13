using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Sites
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteFileVersionBatchDeleteJobProgress")]
    [OutputType(typeof(FileVersionBatchDeleteJobProgress))]
    public class GetSiteFileVersionBatchDeleteJobProgress : PnPSharePointCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var site = ClientContext.Site;
            ClientContext.Load(site, s => s.Url);
            var ret = site.GetProgressForDeleteFileVersions();
            ClientContext.ExecuteQueryRetry();

            var progress = JsonSerializer.Deserialize<FileVersionBatchDeleteJobProgress>(ret.Value);
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
