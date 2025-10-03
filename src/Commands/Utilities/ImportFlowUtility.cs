using PnP.PowerShell.Commands.Utilities.REST;
using PnP.PowerShell.Commands.Base;
using System;
using System.IO;
using System.Text.Json;
using PnP.Framework.Diagnostics;
using System.Net.Http;
using System.Text.Json.Nodes;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class ImportFlowUtility
    {
        public static string GenerateSasUrl(HttpClient httpClient, string accessToken, string baseUrl, string environmentName)
        {
            var response = RestHelper.Post(PnPConnection.Current.HttpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/generateResourceStorage?api-version=2016-11-01", accessToken);
            var data = JsonSerializer.Deserialize<JsonElement>(response);
            return data.GetProperty("sharedAccessSignature").GetString();
        }

        public static UriBuilder BuildBlobUri(string sasUrl, string packagePath)
        {
            var fileName = Path.GetFileName(packagePath);
            var blobUri = new UriBuilder(sasUrl);
            blobUri.Path += $"/{fileName}";
            return blobUri;
        }

        public static void UploadPackageToBlob(UriBuilder blobUri, string PackagePath)
        {
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

        public static System.Net.Http.Headers.HttpResponseHeaders GetImportParameters(HttpClient httpClient, string accessToken, string baseUrl, string environmentName, UriBuilder blobUri)
        {
            var importPayload = new
            {
                packageLink = new
                {
                    value = blobUri.Uri.ToString()
                }
            };
            var response = RestHelper.PostGetResponseHeader<JsonElement>(
                httpClient,
                $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/listImportParameters?api-version=2016-11-01",
                accessToken,
                payload: importPayload,
                accept: "application/json"
            );
            Log.Debug("ImportFlowUtility", "Import parameters retrieved");
            System.Threading.Thread.Sleep(2500);
            return response;
        }

        public static JsonElement GetImportOperations(HttpClient httpClient, string accessToken, string importOperationsUrl)
        {
            var listImportOperations = RestHelper.Get(
                httpClient,
                importOperationsUrl,
                accessToken,
                accept: "application/json"
            );
            Log.Debug("ImportFlowUtility", "Import operations retrieved");
            return JsonSerializer.Deserialize<JsonElement>(listImportOperations);
        }

        public static JsonElement GetPropertiesElement(JsonElement importOperationsData)
        {
            if (!importOperationsData.TryGetProperty("properties", out JsonElement propertiesElement))
            {
                throw new Exception("Import failed: 'properties' section missing.");
            }
            return propertiesElement;
        }

        public static void ValidateProperties(JsonElement propertiesElement)
        {
            bool hasStatus = propertiesElement.TryGetProperty("status", out _);
            bool hasPackageLink = propertiesElement.TryGetProperty("packageLink", out _);
            bool hasDetails = propertiesElement.TryGetProperty("details", out _);
            bool hasResources = propertiesElement.TryGetProperty("resources", out _);

            if (!(hasStatus && hasPackageLink && hasDetails && hasResources))
            {
                throw new Exception("Import failed: One or more required fields are missing in 'properties'.");
            }
            if (!propertiesElement.TryGetProperty("resources", out JsonElement resourcesElement))
            {
                throw new Exception("Import failed: 'resources' section missing in 'properties'.");
            }
        }

        public static JsonObject ParseResources(JsonElement propertiesElement)
        {
            if (!propertiesElement.TryGetProperty("resources", out JsonElement resourcesElement))
            {
                throw new Exception("Import failed: 'resources' section missing in 'properties'.");
            }
            return JsonNode.Parse(resourcesElement.GetRawText()) as JsonObject;
        }

        public static JsonElement ValidateImportPackage(HttpClient httpClient, string accessToken, string baseUrl, string environmentName, JsonObject validatePackagePayload)
        {
            var validateResponse = RestHelper.Post(httpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/validateImportPackage?api-version=2016-11-01", accessToken, payload: validatePackagePayload);
            return JsonSerializer.Deserialize<JsonElement>(validateResponse);
        }

        public static JsonObject TransformResources(JsonObject resourcesObject, string Name)
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
                        if (Name != null)
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

        public static JsonObject CreateImportObject(JsonElement importData, JsonObject resourceObject = null)
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

        public static System.Net.Http.Headers.HttpResponseHeaders ImportPackage(HttpClient httpClient, string accessToken, string baseUrl, string environmentName, JsonObject importPackagePayload)
        {
            var importResult = RestHelper.PostGetResponseHeader<JsonElement>(
                httpClient,
                $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/importPackage?api-version=2016-11-01",
                accessToken,
                payload: importPackagePayload,
                accept: "application/json"
            );
            Log.Debug("ImportFlowUtility", "Import package initiated");
            return importResult;
        }

        public static string WaitForImportCompletion(HttpClient httpClient, string accessToken, string importPackageResponseUrl)
        {
            string status;
            int retryCount = 0;

            do
            {
                System.Threading.Thread.Sleep(2500);
                var importResultData = RestHelper.Get(httpClient, importPackageResponseUrl, accessToken, accept: "application/json");
                var importResultDataElement = JsonSerializer.Deserialize<JsonElement>(importResultData);

                if (importResultDataElement.TryGetProperty("properties", out JsonElement importResultPropertiesElement) &&
                    importResultPropertiesElement.TryGetProperty("status", out JsonElement statusElement))
                {
                    status = statusElement.GetString();
                }
                else
                {
                    Log.Warning("ImportFlowUtility", "Failed to retrieve the status from the response.");
                    throw new Exception("Import status could not be determined.");
                }


                if (status == "Running")
                {
                    Log.Debug("ImportFlowUtility", "Import is still running. Waiting for completion...");
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

        public static void ThrowImportError(string importResultData)
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
