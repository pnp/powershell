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
        private const string ParameterSet_BYIDENTITY = "By Identity";
        private const string ParameterSet_ALL = "All";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYIDENTITY)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_ALL)]
        [Parameter(Mandatory = false)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ALL)]
        public string PackagePath;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ALL)]
        public string Name;

        protected override void ExecuteCmdlet()
        {
            var environmentName = GetEnvironmentName();
            string baseUrl = PowerPlatformUtility.GetBapEndpoint(Connection.AzureEnvironment);

            //Get the SAS URL for the blob storage
            var sasUrl = GenerateSasUrl(baseUrl, environmentName);
            var blobUri = BuildBlobUri(sasUrl, PackagePath);
            // Step 1: Upload the package to the blob storage using the SAS URL
            UploadPackageToBlob(blobUri);
            //Step 2: this will list the import parameters
            var importParametersResponse = GetImportParameters(baseUrl, environmentName, blobUri);
            // Step 3: Get the list of import operations data
            var importOperationsData = GetImportOperations(importParametersResponse.Location.ToString()); 
            var propertiesElement = GetPropertiesElement(importOperationsData);

            ValidateProperties(propertiesElement);
            var resourcesObject = ParseResources(propertiesElement);
            // Step 4: Transform the resources object 
            var resource = TransformResources(resourcesObject);

            var validatePackagePayload = CreateImportObject(propertiesElement, resourcesObject);
            //Step 5: Validate the import package
            var validateResponseData = ValidateImportPackage(baseUrl, environmentName, validatePackagePayload);

            var importPackagePayload = CreateImportObject(validateResponseData);
            //Step 6: import package
            var importResult = ImportPackage(baseUrl, environmentName, importPackagePayload);
            //Step 7: Wait for the import to complete
            var importStatus = WaitForImportCompletion(importResult.Location.ToString());

            WriteObject($"Import {importStatus}");
        }

        private string GetEnvironmentName()
        {
            return ParameterSpecified(nameof(Environment))
                ? Environment.GetName()
                : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
        }

        private string GenerateSasUrl(string baseUrl, string environmentName)
        {
            var response = RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/generateResourceStorage?api-version=2016-11-01", AccessToken);
            WriteVerbose($"Storage resource URL generated: {response}");
            var data = JsonSerializer.Deserialize<JsonElement>(response);
            return data.GetProperty("sharedAccessSignature").GetString();
        }

        private UriBuilder BuildBlobUri(string sasUrl, string packagePath)
        {
            var fileName = Path.GetFileName(packagePath);
            var blobUri = new UriBuilder(sasUrl);
            blobUri.Path += $"/{fileName}";
            return blobUri;
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

        private System.Net.Http.Headers.HttpResponseHeaders GetImportParameters(string baseUrl, string environmentName, UriBuilder blobUri)
        {
            var importPayload = new
            {
                packageLink = new
                {
                    value = blobUri.Uri.ToString()
                }
            };
            var response = RestHelper.PostGetResponseHeader<JsonElement>(
                Connection.HttpClient,
                $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/listImportParameters?api-version=2016-11-01",
                AccessToken,
                payload: importPayload,
                accept: "application/json"
            );
            WriteVerbose("Import parameters retrieved");
            System.Threading.Thread.Sleep(2500);
            return response;
        }

        private JsonElement GetImportOperations(string importOperationsUrl)
        {
            var listImportOperations = RestHelper.Get(
                Connection.HttpClient,
                importOperationsUrl,
                AccessToken,
                accept: "application/json"
            );
            WriteVerbose("Import operations retrieved");
            return JsonSerializer.Deserialize<JsonElement>(listImportOperations);
        }

        private JsonElement GetPropertiesElement(JsonElement importOperationsData)
        {
            if (!importOperationsData.TryGetProperty("properties", out JsonElement propertiesElement))
            {
                WriteObject("Import failed: 'properties' section missing.");
                throw new Exception("Import failed: 'properties' section missing.");
            }
            return propertiesElement;
        }

        private void ValidateProperties(JsonElement propertiesElement)
        {
            bool hasStatus = propertiesElement.TryGetProperty("status", out _);
            bool hasPackageLink = propertiesElement.TryGetProperty("packageLink", out _);
            bool hasDetails = propertiesElement.TryGetProperty("details", out _);
            bool hasResources = propertiesElement.TryGetProperty("resources", out _);

            if (!(hasStatus && hasPackageLink && hasDetails && hasResources))
            {
                WriteObject("Import failed: One or more required fields are missing in 'properties'.");
                throw new Exception("Import failed: One or more required fields are missing in 'properties'.");
            }
            if (!propertiesElement.TryGetProperty("resources", out JsonElement resourcesElement))
            {
                WriteObject("Import failed: 'resources' section missing in 'properties'.");
                return;
            }
        }

        private JsonObject ParseResources(JsonElement propertiesElement)
        {
            if (!propertiesElement.TryGetProperty("resources", out JsonElement resourcesElement))
            {
                WriteObject("Import failed: 'resources' section missing in 'properties'.");
                throw new Exception("Import failed: 'resources' section missing in 'properties'.");
            }
            return JsonNode.Parse(resourcesElement.GetRawText()) as JsonObject;
        }

        private JsonElement ValidateImportPackage(string baseUrl, string environmentName, JsonObject validatePackagePayload)
        {
            var validateResponse = RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/validateImportPackage?api-version=2016-11-01", AccessToken, payload: validatePackagePayload);
            return JsonSerializer.Deserialize<JsonElement>(validateResponse);
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
                        resource["selectedCreationType"] = "New";
                        if (ParameterSpecified(nameof(Name)))
                        {
                            if (resource.TryGetPropertyValue("details", out JsonNode detailsNode) && detailsNode is JsonObject detailsObject)
                            {
                                detailsObject["displayName"] = Name;
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

        private System.Net.Http.Headers.HttpResponseHeaders ImportPackage(string baseUrl, string environmentName, JsonObject importPackagePayload)
        {
            var importResult = RestHelper.PostGetResponseHeader<JsonElement>(
                Connection.HttpClient,
                $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/importPackage?api-version=2016-11-01",
                AccessToken,
                payload: importPackagePayload,
                accept: "application/json"
            );
            WriteVerbose("Import package initiated");
            return importResult;
        }

        private string WaitForImportCompletion(string importPackageResponseUrl)
        {
            string status;
            int retryCount = 0;

            do
            {
                System.Threading.Thread.Sleep(2500);
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
                    retryCount++;
                }
                else if (status == "Failed")
                {
                    ThrowImportError(importResultData);
                }
            } while (status == "Running" && retryCount < 5);

            if (status == "Running")
            {
                throw new Exception("Import failed to complete after 5 attempts.");
            }

            return status;
        }

        private void ThrowImportError(string importResultData)
        {
            var importErrorResultData = JsonSerializer.Deserialize<JsonElement>(importResultData);
            if (importErrorResultData.TryGetProperty("properties", out JsonElement importErrorResultPropertiesElement) &&
                importErrorResultPropertiesElement.TryGetProperty("resources", out JsonElement resourcesElement))
            {
                foreach (var resource in resourcesElement.EnumerateObject())
                {
                    if (resource.Value.TryGetProperty("error", out JsonElement errorElement))
                    {
                        string errorMessage = errorElement.TryGetProperty("message", out JsonElement messageElement)
                            ? messageElement.GetString()
                            : errorElement.TryGetProperty("code", out JsonElement codeElement)
                                ? codeElement.GetString()
                                : "Unknown error";
                        throw new Exception($"Import failed: {errorMessage}");
                    }
                }
                throw new Exception("Import failed: No error details found in resources.");
            }
            else
            {
                throw new Exception("Import failed: Unknown error.");
            }
        }
    }
}
