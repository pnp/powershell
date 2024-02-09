using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using PnP.Framework;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.Utilities
{
    internal static class PowerAppsUtility
    {
        internal static async Task<Model.PowerPlatform.PowerApp.PowerAppPackageWrapper> GetWrapper(HttpClient connection, string environmentName, string accessToken, string appName, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
        {
            var postData = new
            {
                baseResourceIds = new[] {
                    $"/providers/Microsoft.PowerApps/apps/{appName}"
                }
            };
            string baseUrl = PowerPlatformUtility.GetBapEndpoint(azureEnvironment);
            var wrapper = await RestHelper.PostAsync<Model.PowerPlatform.PowerApp.PowerAppPackageWrapper>(connection, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/listPackageResources?api-version=2016-11-01", accessToken, payload: postData);


            return wrapper;
        }

        internal static HttpResponseHeaders GetResponseHeader(HttpClient connection, string environmentName, string accessToken, string appName, Model.PowerPlatform.PowerApp.PowerAppPackageWrapper wrapper, object details, AzureEnvironment azureEnvironment = AzureEnvironment.Production)
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
            var responseHeader = RestHelper.PostAsyncGetResponseHeader<string>(connection, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/exportPackage?api-version=2016-11-01", accessToken, payload: exportPostData).GetAwaiter().GetResult();


            return responseHeader;
        }

        internal static string GetPackageLink(HttpClient connection, string location, string accessToken)
        {
            var status = Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Running;
            var packageLink = "";
            if (location != null)
            {
                do
                {
                    var runningresponse = RestHelper.GetAsync<JsonElement>(connection, location, accessToken).GetAwaiter().GetResult();

                    if (runningresponse.TryGetProperty("properties", out JsonElement properties))
                    {
                        if (properties.TryGetProperty("status", out JsonElement runningstatusElement))
                        {
                            if (runningstatusElement.GetString() == Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Succeeded.ToString())
                            {
                                status = Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Succeeded;
                                if (properties.TryGetProperty("packageLink", out JsonElement packageLinkElement))
                                {
                                    if (packageLinkElement.TryGetProperty("value", out JsonElement valueElement))
                                    {
                                        packageLink = valueElement.GetString();
                                    }
                                }
                            }
                            else
                            {
                                //if status is still running, sleep the thread for 3 seconds
                                Thread.Sleep(3000);
                            }
                        }
                    }
                } while (status == Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Running);
            }
            return packageLink;
        }

        internal static byte[] GetFileByteArray(HttpClient connection, string packageLink, string accessToken)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, packageLink))
            {
                requestMessage.Version = new Version(2, 0);
                //requestMessage.Headers.Add("Authorization", $"Bearer {AccessToken}");
                var fileresponse = connection.SendAsync(requestMessage).GetAwaiter().GetResult();
                var byteArray = fileresponse.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                return byteArray;
            }

        }
    }
}
