using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using PnP.PowerShell.Commands.Model;
using System.Linq;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class CopyMover
    {
        internal static async Task<List<CopyJobLog>> CopyAsync(HttpClient httpClient, string accessToken, Uri currentContextUri, string fullyQualifiedSourceUrl, string fullyQualifiedDestinationUrl, bool ignoreVersionHistory, bool overwrite, bool allowSchemaMismatch, bool sameWebCopyMoveOptimization, bool allowSmallerVersionLimitOnDestination)
        {
            return await BaseRequestAsync(httpClient, accessToken, false, currentContextUri, fullyQualifiedSourceUrl, fullyQualifiedDestinationUrl, ignoreVersionHistory, overwrite, allowSchemaMismatch, sameWebCopyMoveOptimization, allowSmallerVersionLimitOnDestination);
        }

        internal static async Task<List<CopyJobLog>> MoveAsync(HttpClient httpClient, string accessToken, Uri currentContextUri,string fullyQualifiedSourceUrl, string fullyQualifiedDestinationUrl, bool ignoreVersionHistory, bool overwrite, bool allowSchemaMismatch, bool sameWebCopyMoveOptimization, bool allowSmallerVersionLimitOnDestination)
        {
            return await BaseRequestAsync(httpClient, accessToken, true, currentContextUri, fullyQualifiedSourceUrl, fullyQualifiedDestinationUrl, ignoreVersionHistory, overwrite, allowSchemaMismatch, sameWebCopyMoveOptimization, allowSmallerVersionLimitOnDestination);
        }

        private static async Task<List<CopyJobLog>> BaseRequestAsync(HttpClient httpClient, string accessToken, bool isMove, Uri currentContextUri, string fullyQualifiedSourceUrl, string fullyQualifiedDestinationUrl, bool ignoreVersionHistory, bool overwrite, bool allowSchemaMismatch, bool sameWebCopyMoveOptimization, bool allowSmallerVersionLimitOnDestination)
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


            var results = await REST.RestHelper.PostAsync<REST.RestResultCollection<Model.CopyMigrationInfo>>(httpClient, $"{currentContextUri}/_api/site/CreateCopyJobs", accessToken, body, false);

            if (results != null && results.Items.Any())
            {
                var result = results.Items.First();

                var copyJobInfo = new
                {
                    copyJobInfo = new
                    {
                        EncryptionKey = result.EncryptionKey,
                        JobId = result.JobId,
                        JobQueueUri = result.JobQueueUri,
                        SourceListItemUniqueIds = result.SourceListItemUniqueIds
                    }
                };
                var copyJob = await Utilities.REST.RestHelper.PostAsync<CopyMigrationJob>(httpClient, $"{currentContextUri}/_api/site/GetCopyJobProgress", accessToken, copyJobInfo, false);
                if (copyJob != null)
                {
                    while (copyJob.JobState != 0)
                    {
                        // sleep 5 seconds
                        System.Threading.Thread.Sleep(5000);
                        copyJob = await Utilities.REST.RestHelper.PostAsync<CopyMigrationJob>(httpClient, $"{currentContextUri}/_api/site/GetCopyJobProgress", accessToken, copyJobInfo, false);
                    }
                    foreach (var log in copyJob.Logs)
                    {
                        var copyLog = JsonSerializer.Deserialize<CopyJobLog>(log);
                        logs.Add(copyLog);
                    }
                }
            }
            return logs;
        }
    }
}