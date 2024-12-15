using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base.Completers;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.SharePoint;
using System;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.Lists
{
    [Cmdlet(VerbsCommon.Get, "PnPLibraryFileVersionBatchDeleteJobStatus")]
    [OutputType(typeof(FileVersionBatchDeleteJobStatus))]
    public class GetLibraryFileVersionBatchDeleteJobStatus : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [ValidateNotNull]
        [ArgumentCompleter(typeof(ListNameCompleter))]
        public ListPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var list = Identity.GetList(CurrentWeb);
            if (list != null)
            {
                var ret = list.GetProgressForDeleteFileVersions();
                ClientContext.ExecuteQueryRetry();

                var progress = JsonSerializer.Deserialize<FileVersionBatchDeleteJobStatus>(ret.Value);
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
