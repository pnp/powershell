using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PnP.PowerShell.Commands.Utilities
{
    /// <summary>
    /// Shared logic for the Power Platform export cmdlets to run an export and to describe why the service refused it
    /// </summary>
    internal static class PowerPlatformExportUtility
    {
        /// <summary>
        /// Runs the provided export and turns the failures that are to be expected during an export into a message handed to
        /// <paramref name="writeError"/>, so a batch export can continue with the next item. Any other exception is left to bubble up.
        /// </summary>
        /// <param name="export">The export to run</param>
        /// <param name="writeError">Called with the description of the failure when the export did not succeed</param>
        internal static void RunExport(Action export, Action<string> writeError)
        {
            try
            {
                export();
            }
            catch (HttpRequestException ex)
            {
                writeError(ex.Message);
            }
            catch (TaskCanceledException ex)
            {
                writeError($"The request timed out. {ex.Message}");
            }
            catch (UriFormatException ex)
            {
                writeError($"The service returned an invalid package link. {ex.Message}");
            }
            catch (System.IO.IOException ex)
            {
                writeError($"The package file could not be written. {ex.Message}");
            }
            catch (UnauthorizedAccessException ex)
            {
                writeError($"The package file could not be written. {ex.Message}");
            }
            catch (PSInvalidOperationException ex)
            {
                writeError(ex.Message);
            }
        }

        /// <summary>
        /// Describes why an export failed based on the errors reported by the service, falling back to the status when the
        /// service did not report any error details
        /// </summary>
        /// <param name="errors">The error codes and messages reported by the service</param>
        /// <param name="status">The status reported by the service</param>
        internal static string DescribeFailure(IEnumerable<(string Code, string Message)> errors, string status)
        {
            var details = errors?
                .Where(error => !string.IsNullOrWhiteSpace(error.Code) || !string.IsNullOrWhiteSpace(error.Message))
                .Select(error => FormatError(error.Code, error.Message))
                .ToArray();

            if (details?.Length > 0)
            {
                return string.Join("; ", details);
            }

            return $"The service returned status '{(string.IsNullOrWhiteSpace(status) ? "unknown" : status)}' without error details.";
        }

        /// <summary>
        /// Describes why an export failed based on the errors in a raw service response
        /// </summary>
        /// <param name="response">The response returned by the service</param>
        /// <param name="status">The status reported by the service</param>
        internal static string DescribeFailure(JsonElement response, string status)
        {
            var errors = new List<(string Code, string Message)>();
            if (response.ValueKind == JsonValueKind.Object && response.TryGetProperty("errors", out JsonElement errorsElement) && errorsElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var errorElement in errorsElement.EnumerateArray())
                {
                    if (errorElement.ValueKind != JsonValueKind.Object)
                    {
                        continue;
                    }

                    errors.Add((GetStringProperty(errorElement, "code"), GetStringProperty(errorElement, "message")));
                }
            }

            return DescribeFailure(errors, status);
        }

        /// <summary>
        /// Returns the name of the ZIP file contained in the package link returned by the service, or an empty string when it does not contain one
        /// </summary>
        /// <param name="packageLink">The package link returned by the service</param>
        internal static string GetFileNameFromPackageLink(string packageLink)
        {
            return new System.Text.RegularExpressions.Regex("([^\\/]+\\.zip)").Match(packageLink ?? string.Empty).Value;
        }

        /// <summary>
        /// Returns the value of the requested property when it is present and holds a string, otherwise null
        /// </summary>
        /// <param name="element">The element to read the property from</param>
        /// <param name="propertyName">Name of the property to read</param>
        internal static string GetStringProperty(JsonElement element, string propertyName)
        {
            return element.TryGetProperty(propertyName, out JsonElement property) && property.ValueKind == JsonValueKind.String
                ? property.GetString()
                : null;
        }

        private static string FormatError(string code, string message)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return message;
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                return code;
            }
            return $"{code}: {message}";
        }
    }
}
