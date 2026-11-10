using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Management.Automation;
using PnP.Framework;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class PowerAppsUtility
    {
        /// <summary>
        /// How long to keep waiting for an export to complete. Exporting a large Power App takes a while, so this is generous.
        /// </summary>
        private static readonly TimeSpan ExportStatusMaxWaitTime = TimeSpan.FromMinutes(30);

        /// <summary>
        /// How long to wait between two export status checks when the service does not ask for a specific delay through a Retry-After header
        /// </summary>
        private static readonly TimeSpan ExportStatusPollingDelay = TimeSpan.FromSeconds(3);

        /// <summary>
        /// Upper bound on the delay taken from a Retry-After header, so one unexpected value cannot stall the export
        /// </summary>
        private static readonly TimeSpan ExportStatusMaxPollingDelay = TimeSpan.FromSeconds(60);

        internal static Model.PowerPlatform.PowerApp.PowerAppPackageWrapper GetWrapper(HttpClient connection, string environmentName, string accessToken, string appName, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            var postData = new
            {
                baseResourceIds = new[] {
                    $"/providers/Microsoft.PowerApps/apps/{appName}"
                }
            };
            string baseUrl = PowerPlatformUtility.GetBapEndpoint(azureEnvironment);
            var wrapper = RestHelper.Post<Model.PowerPlatform.PowerApp.PowerAppPackageWrapper>(connection, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/listPackageResources?api-version=2016-11-01", accessToken, payload: postData);


            return wrapper;
        }

        internal static Uri GetResponseLocation(HttpClient connection, string environmentName, string accessToken, string appName, Model.PowerPlatform.PowerApp.PowerAppPackageWrapper wrapper, object details, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            var exportPostData = new
            {
                includedResourceIds = new[]
                 {
                             $"/providers/Microsoft.PowerApps/apps/{appName}"
                },
                details = details,
                resources = wrapper.Resources
            };

            string baseUrl = PowerPlatformUtility.GetBapEndpoint(azureEnvironment);
            var responseLocation = RestHelper.PostGetResponseLocation<string>(connection, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/exportPackage?api-version=2016-11-01", accessToken, payload: exportPostData);


            return responseLocation;
        }

        internal static string GetPackageLink(HttpClient connection, string location, string accessToken)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                throw new PSInvalidOperationException("The Power App export response did not include a status location.");
            }

            var deadline = DateTimeOffset.UtcNow.Add(ExportStatusMaxWaitTime);
            while (true)
            {
                var runningResponse = RestHelper.Get<JsonElement>(connection, location, accessToken, out TimeSpan? retryAfter);
                if (runningResponse.ValueKind != JsonValueKind.Object ||
                    !runningResponse.TryGetProperty("properties", out JsonElement properties) ||
                    !properties.TryGetProperty("status", out JsonElement statusElement) ||
                    statusElement.ValueKind != JsonValueKind.String)
                {
                    throw new PSInvalidOperationException("The Power App export status response was incomplete.");
                }

                var status = statusElement.GetString();
                if (string.Equals(status, Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Succeeded.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    if (properties.TryGetProperty("packageLink", out JsonElement packageLinkElement) &&
                        packageLinkElement.ValueKind == JsonValueKind.Object &&
                        packageLinkElement.TryGetProperty("value", out JsonElement valueElement) &&
                        valueElement.ValueKind == JsonValueKind.String &&
                        !string.IsNullOrWhiteSpace(valueElement.GetString()))
                    {
                        return valueElement.GetString();
                    }

                    throw new PSInvalidOperationException("The Power App export completed without a package link.");
                }

                if (!string.Equals(status, Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Running.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    throw new PSInvalidOperationException($"The Power App export returned status '{status ?? "unknown"}'.");
                }

                // Follow the delay the service asks for, but never wait longer than the upper bound for a single check
                var delay = retryAfter ?? ExportStatusPollingDelay;
                if (delay > ExportStatusMaxPollingDelay)
                {
                    delay = ExportStatusMaxPollingDelay;
                }

                if (DateTimeOffset.UtcNow.Add(delay) >= deadline)
                {
                    throw new PSInvalidOperationException($"The Power App export did not complete within {ExportStatusMaxWaitTime.TotalMinutes:0} minutes.");
                }

                Thread.Sleep(delay);
            }
        }
    }
}
