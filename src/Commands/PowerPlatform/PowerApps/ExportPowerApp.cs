using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using System.Threading;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsData.Export, "PnPPowerApp")]
    [RequiredMinimalApiPermissions("https://management.azure.com/.default")]
    public class ExportPowerApp : PnPAzureManagementApiCmdlet
    {

        [Parameter(Mandatory = true)]

        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAppPipeBind Identity;

        [Parameter(Mandatory = false)]
        public string PackageDisplayName;

        [Parameter(Mandatory = false)]
        public string PackageDescription;

        [Parameter(Mandatory = false)]
        public string PackageCreatedBy;

        [Parameter(Mandatory = false)]
        public string PackageSourceEnvironment;

        [Parameter(Mandatory = false)]
        public string OutPath;

        [Parameter(Mandatory = false)]
        public SwitchParameter Force;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(OutPath)))
            {
                if (!System.IO.Path.IsPathRooted(OutPath))
                {
                    OutPath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, OutPath);
                }
                if (System.IO.Directory.Exists(OutPath))
                {
                    throw new PSArgumentException("Please specify a folder including a filename");
                }
                if (System.IO.File.Exists(OutPath))
                {
                    if (!Force && !ShouldContinue($"File '{OutPath}' exists. Overwrite?", "Export App"))
                    {
                        // Exit cmdlet
                        return;
                    }
                }
            }

            var environmentName = Environment.GetName();
            var appName = Identity.GetName();

            var postData = new
            {
                baseResourceIds = new[] {
                    $"/providers/Microsoft.PowerApps/apps/{appName}"
                }
            };
            var wrapper = RestHelper.PostAsync<Model.PowerPlatform.PowerApp.PowerAppPackageWrapper>(Connection.HttpClient, $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/listPackageResources?api-version=2016-11-01", AccessToken, payload: postData).GetAwaiter().GetResult();

            if (wrapper.Status == Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Succeeded)
            { 
                foreach (var resource in wrapper.Resources)
                {
                    if (resource.Value.Type == "Microsoft.PowerApps/apps")
                    {
                        resource.Value.SuggestedCreationType = "Update";
                    }
                }
            var exportPostData = new
            {
                includedResourceIds = new[]
                {
                             $"/providers/Microsoft.PowerApps/apps/{appName}"
                },
                details = new
                {
                    displayName = PackageDisplayName,
                    description = PackageDescription,
                    creator = PackageCreatedBy,
                    sourceEnvironment = PackageSourceEnvironment
                },
                resources = wrapper.Resources
            };

            var responseHeader = RestHelper.PostAsyncGetResponseHeader<string>(Connection.HttpClient, $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/exportPackage?api-version=2016-11-01", AccessToken, payload: exportPostData).GetAwaiter().GetResult();
            
            var packageLink = getPackageLink(Convert.ToString(responseHeader.Location));
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, packageLink))
            {
                requestMessage.Version = new Version(2, 0);
                //requestMessage.Headers.Add("Authorization", $"Bearer {AccessToken}");
                var fileresponse = Connection.HttpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
                var byteArray = fileresponse.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
                var fileName = string.Empty;
                if (ParameterSpecified(nameof(OutPath)))
                {
                    if (!System.IO.Path.IsPathRooted(OutPath))
                    {
                        OutPath = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, OutPath);
                    }
                    fileName = OutPath;
                }
                else
                {
                    fileName = new System.Text.RegularExpressions.Regex("([^\\/]+\\.zip)").Match(packageLink).Value;
                    fileName = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, fileName);
                }

                System.IO.File.WriteAllBytes(fileName, byteArray);
                var returnObject = new PSObject();
                returnObject.Properties.Add(new PSNoteProperty("Filename", fileName));
                WriteObject(returnObject);
            }

            }
            else
            {
                // Errors have been reported in the export request result
                foreach (var error in wrapper.Errors)
                {
                    WriteVerbose($"Export failed for {appName} with error {error.Code}: {error.Message}");
                }
            }
        }
        private string getPackageLink(string location)
        {
            var status = Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Running;
            var packageLink = "";
            if (location != null)
            {
                do
                {
                    var runningresponse = RestHelper.GetAsync<JsonElement>(Connection.HttpClient, location, AccessToken).GetAwaiter().GetResult();

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
    }
}
