using Microsoft.SharePoint.Client;

using PnP.Framework.Utilities;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPLibraryFileVersionBatchDeleteJobProgress")]
    [OutputType(typeof(FileVersionBatchDeleteJobProgress))]
    public class GetLibraryFileVersionBatchDeleteJobProgress : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ValidateNotNull]
        public ListPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);
            if (list != null)
            {
                var ret = list.GetProgressForDeleteFileVersions();
                ClientContext.ExecuteQueryRetry();

                var progress = JsonSerializer.Deserialize<FileVersionBatchDeleteJobProgress>(ret.Value);
                var connectionUrl = new Uri(Connection.Url);
                var serverUrl = string.Concat("https://", connectionUrl.Host);
                progress.Url = string.Concat(serverUrl, list.RootFolder.ServerRelativeUrl);

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
}
