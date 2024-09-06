using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PnP.PowerShell.Commands.Model;
using System.Linq;
using Microsoft.SharePoint.Client;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class CopyMover
    {
        internal static async Task<(List<CopyJobLog> logs, Model.CopyMigrationInfo jobInfo)> CopyAsync(HttpClient httpClient, ClientContext clientContext, Uri currentContextUri, string fullyQualifiedSourceUrl, string fullyQualifiedDestinationUrl, bool ignoreVersionHistory, bool overwrite, bool allowSchemaMismatch, bool sameWebCopyMoveOptimization, bool allowSmallerVersionLimitOnDestination, bool noWait)
        {
            return await BaseRequestAsync(httpClient, clientContext, false, currentContextUri, fullyQualifiedSourceUrl, fullyQualifiedDestinationUrl, ignoreVersionHistory, overwrite, allowSchemaMismatch, sameWebCopyMoveOptimization, allowSmallerVersionLimitOnDestination, noWait);
        }

        internal static async Task<(List<CopyJobLog> logs, Model.CopyMigrationInfo jobInfo)> MoveAsync(HttpClient httpClient, ClientContext clientContext, Uri currentContextUri, string fullyQualifiedSourceUrl, string fullyQualifiedDestinationUrl, bool ignoreVersionHistory, bool overwrite, bool allowSchemaMismatch, bool sameWebCopyMoveOptimization, bool allowSmallerVersionLimitOnDestination, bool noWait)
        {
            return await BaseRequestAsync(httpClient, clientContext, true, currentContextUri, fullyQualifiedSourceUrl, fullyQualifiedDestinationUrl, ignoreVersionHistory, overwrite, allowSchemaMismatch, sameWebCopyMoveOptimization, allowSmallerVersionLimitOnDestination, noWait);
        }

        private static async Task<(List<CopyJobLog> logs, Model.CopyMigrationInfo jobInfo)> BaseRequestAsync(HttpClient httpClient, ClientContext clientContext, bool isMove, Uri currentContextUri, string fullyQualifiedSourceUrl, string fullyQualifiedDestinationUrl, bool ignoreVersionHistory, bool overwrite, bool allowSchemaMismatch, bool sameWebCopyMoveOptimization, bool allowSmallerVersionLimitOnDestination, bool noWait)
        {
            var logs = new List<CopyJobLog>();

            var body = new
            {
                exportObjectUris = new[] {
                        fullyQualifiedSourceUrl
                    },
                destinationUri = fullyQualifiedDestinationUrl,
                options = new
                {
                    IgnoreVersionHistory = ignoreVersionHistory,
                    IsMoveMode = isMove,
                    NameConflictBehavior = overwrite ? 1 : 0,
                    AllowSchemaMismatch = allowSchemaMismatch,
                    SameWebCopyMoveOptimization = sameWebCopyMoveOptimization,
                    AllowSmallerVersionLimitOnDestination = allowSmallerVersionLimitOnDestination
                }
            };

            var results =  REST.RestHelper.Post<REST.RestResultCollection<Model.CopyMigrationInfo>>(httpClient, $"{currentContextUri}/_api/site/CreateCopyJobs", clientContext, body, false);

            if (results != null && results.Items.Any())
            {
                var result = results.Items.First();

                var copyJobInfo = new
                {
                    copyJobInfo = result
                };
                var copyJob = Utilities.REST.RestHelper.Post<CopyMigrationJob>(httpClient, $"{currentContextUri}/_api/site/GetCopyJobProgress", clientContext, copyJobInfo, false);
                if (copyJob != null)
                {
                    if (noWait)
                    {
                        return (null, result);
                    }
                    while (copyJob.JobState != 0)
                    {
                        // sleep 1 second
                        await Task.Delay(1000);
                        copyJob = Utilities.REST.RestHelper.Post<CopyMigrationJob>(httpClient, $"{currentContextUri}/_api/site/GetCopyJobProgress", clientContext, copyJobInfo, false);
                    }
                    foreach (var log in copyJob.Logs)
                    {
                        var copyLog = JsonSerializer.Deserialize<CopyJobLog>(log);
                        logs.Add(copyLog);
                    }
                }
            }
            return (logs, null);
        }

        public static async Task<CopyMigrationJob> GetCopyMigrationJobStatusAsync(HttpClient httpClient, Uri currentContextUri, ClientContext clientContext, Model.CopyMigrationInfo jobInfo, bool noWait)
        {
            var logs = new List<CopyJobLog>();

            var copyJobInfo = new
            {
                copyJobInfo = jobInfo
            };
            var copyJob = Utilities.REST.RestHelper.Post<CopyMigrationJob>(httpClient, $"{currentContextUri}/_api/site/GetCopyJobProgress", clientContext, copyJobInfo, false);
            if (copyJob != null)
            {
                if (!noWait)
                {
                    while (copyJob.JobState != 0)
                    {
                        // sleep 1 second
                        await Task.Delay(1000);
                        copyJob = Utilities.REST.RestHelper.Post<CopyMigrationJob>(httpClient, $"{currentContextUri}/_api/site/GetCopyJobProgress", clientContext, copyJobInfo, false);
                    }
                }
            }
            return copyJob;
        }
    }
}