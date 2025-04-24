using Microsoft.Graph;
using Newtonsoft.Json.Serialization;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.IO;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsData.Import, "PnPFlow")]
    [ApiNotAvailableUnderApplicationPermissions]
    [RequiredApiDelegatedPermissions("azure/user_impersonation")]
    public class ImportFlow : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public string PackagePath;

        [Parameter(Mandatory = false)]
        public SwitchParameter CreateAsNew;

        [Parameter(Mandatory = false)]
        public string Name;

        protected override void ExecuteCmdlet()
        {
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;

            string baseUrl = PowerPlatformUtility.GetBapEndpoint(Connection.AzureEnvironment);
            // Step 1: Generate a storage URL for the package
            var generateResourceUrlResponse = RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/generateResourceStorage?api-version=2016-11-01", AccessToken);
            WriteVerbose($"Storage resource URL generated: {generateResourceUrlResponse}");

            // Parse the response to get the shared access signature URL
            var resourceUrlData = JsonSerializer.Deserialize<JsonElement>(generateResourceUrlResponse);
            var sasUrl = resourceUrlData.GetProperty("sharedAccessSignature").GetString();


            var fileName = Path.GetFileName(PackagePath);
            var blobUri = new UriBuilder(sasUrl);
            blobUri.Path += $"/{fileName}";

            UploadPackageToBlob(blobUri);


            // Step 3: Get import parameters with the package link
            var importPayload = new
            {
                packageLink = new
                {
                    value = blobUri.Uri.ToString()
                }
            };

            var importParametersResponse = RestHelper.PostGetResponseHeader<JsonElement>(
                Connection.HttpClient,
                $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/listImportParameters?api-version=2016-11-01",
                AccessToken,
                payload: importPayload,
                accept: "application/json"
            );
            WriteVerbose("Import parameters retrieved");

            System.Threading.Thread.Sleep(2500); //Wait 2.5 seconds to get the import parameters

            var importOperationsUrl = importParametersResponse.Location.ToString();

            var listImportOperations = RestHelper.Get(
                Connection.HttpClient,
                importOperationsUrl,
                AccessToken,
                accept: "application/json"
            );

            WriteVerbose("Import operations retrieved");

            var importOperationsData = JsonSerializer.Deserialize<JsonElement>(listImportOperations);

            if (!importOperationsData.TryGetProperty("properties", out JsonElement propertiesElement))
            {
                WriteObject("Import failed: 'properties' section missing.");
                return;
            }

            bool hasStatus = propertiesElement.TryGetProperty("status", out _);
            bool hasPackageLink = propertiesElement.TryGetProperty("packageLink", out _);
            bool hasDetails = propertiesElement.TryGetProperty("details", out _);
            bool hasResources = propertiesElement.TryGetProperty("resources", out _);

            if (!(hasStatus && hasPackageLink && hasDetails && hasResources))
            {
                WriteObject("Import failed: One or more required fields are missing in 'properties'.");
                return;
            }
            if (!propertiesElement.TryGetProperty("resources", out JsonElement resourcesElement))
            {
                WriteObject("Import failed: 'resources' section missing in 'properties'.");
                return;
            }

            var resourcesObject = JsonNode.Parse(resourcesElement.GetRawText()) as JsonObject;
            var resource = TransformResources(resourcesObject);

            // Update the "resources" in the propertiesElement
            var validatePackagePayload = CreateImportObject(propertiesElement, resourcesObject);

            var validateResponse = RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/validateImportPackage?api-version=2016-11-01", AccessToken, payload: validatePackagePayload);
            var validateResponseData = JsonSerializer.Deserialize<JsonElement>(validateResponse);

            var importPackagePayload = CreateImportObject(validateResponseData);

            var importResult = RestHelper.PostGetResponseHeader<JsonElement>(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/importPackage?api-version=2016-11-01", AccessToken, payload: importPackagePayload, accept: "application/json");
            WriteVerbose("Import package initiated");

            var importPackageResponseUrl = importResult.Location.ToString();

            var importStatus = WaitForImportCompletion(importPackageResponseUrl);

            WriteObject($"Import {importStatus}");
        }

        private void UploadPackageToBlob(UriBuilder blobUri)
        {
            // Step 2: Upload the package to the blob storage using the SAS URL

            // Upload using clean HttpClient
            using (var blobClient = new HttpClient())
            using (var packageFileStream = new FileStream(PackagePath, FileMode.Open, FileAccess.Read))
            {
                var packageContent = new StreamContent(packageFileStream);
                packageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                var request = new HttpRequestMessage(HttpMethod.Put, blobUri.Uri)
                {
                    Content = packageContent
                };

                request.Headers.Add("x-ms-blob-type", "BlockBlob");

                var uploadResponse = blobClient.SendAsync(request).GetAwaiter().GetResult();

                if (!uploadResponse.IsSuccessStatusCode)
                {
                    var errorContent = uploadResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    throw new Exception($"Upload failed: {uploadResponse.StatusCode} - {errorContent}");
                }
            }
        }

        private JsonObject TransformResources(JsonObject resourcesObject)
        {
            foreach (var property in resourcesObject)
            {
                string resourceKey = property.Key;
                var resource = property.Value as JsonObject;

                if (resource != null && resource.TryGetPropertyValue("type", out JsonNode typeNode))
                {
                    string resourceType = typeNode?.ToString();

                    if (resourceType == "Microsoft.Flow/flows")
                    {
                        if (CreateAsNew)
                        {
                            resource["selectedCreationType"] = "New";
                            if (ParameterSpecified(nameof(Name)))
                            {
                                if (resource.TryGetPropertyValue("details", out JsonNode detailsNode) && detailsNode is JsonObject detailsObject)
                                {
                                    detailsObject["displayName"] = Name;
                                }
                            }

                        }
                        else
                        {
                            resource["selectedCreationType"] = "Existing";
                            if (resource.TryGetPropertyValue("suggestedId", out JsonNode suggestedIdNode) && suggestedIdNode != null)
                            {
                                resource["id"] = JsonValue.Create(suggestedIdNode.ToString());
                            }
                        }
                    }
                    else if (resourceType == "Microsoft.PowerApps/apis/connections")
                    {
                        resource["selectedCreationType"] = "Existing";

                        // Only set the id if suggestedId exists
                        if (resource.TryGetPropertyValue("suggestedId", out JsonNode suggestedIdNode) && suggestedIdNode != null)
                        {
                            resource["id"] = JsonValue.Create(suggestedIdNode.ToString());
                        }
                    }
                }
            }
            return resourcesObject;
        }

        private JsonObject CreateImportObject(JsonElement importData, JsonObject resourceObject = null)
        {
            JsonObject resourcesObject = new JsonObject
            {
                ["details"] = JsonNode.Parse(importData.GetProperty("details").GetRawText()),
                ["packageLink"] = JsonNode.Parse(importData.GetProperty("packageLink").GetRawText()),
                ["status"] = JsonNode.Parse(importData.GetProperty("status").GetRawText()),
                ["resources"] = resourceObject ?? JsonNode.Parse(importData.GetProperty("resources").GetRawText())
            };
            return resourcesObject;
        }

        private string WaitForImportCompletion(string importPackageResponseUrl)
        {
            string status;
            int retryCount = 0;

            do
            {
                var importResultData = RestHelper.Get(Connection.HttpClient, importPackageResponseUrl, AccessToken, accept: "application/json");
                var importResultDataElement = JsonSerializer.Deserialize<JsonElement>(importResultData);

                if (importResultDataElement.TryGetProperty("properties", out JsonElement importResultPropertiesElement) &&
                    importResultPropertiesElement.TryGetProperty("status", out JsonElement statusElement))
                {
                    status = statusElement.GetString();
                }
                else
                {
                    WriteWarning("Failed to retrieve the status from the response.");
                    throw new Exception("Import status could not be determined.");
                }

                if (status == "Running")
                {
                    WriteVerbose("Import is still running. Waiting for completion...");
                    System.Threading.Thread.Sleep(2500); // Wait for 2.5 seconds before retrying
                    retryCount++;
                }
            } while (status == "Running" && retryCount < 5);

            if (status == "Running")
            {
                throw new Exception("Import failed to complete after 5 attempts.");
            }

            return status;
        }
    }
}
