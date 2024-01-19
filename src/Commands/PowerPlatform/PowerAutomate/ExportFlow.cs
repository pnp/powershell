﻿using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsData.Export, "PnPFlow")]
    public class ExportFlow : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_ASJSON = "As Json";
        private const string ParameterSet_ASPACKAGE = "As ZIP Package";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASPACKAGE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASJSON)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASPACKAGE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASJSON)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_ASPACKAGE)]
        public SwitchParameter AsZipPackage;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASPACKAGE)]
        public string PackageDisplayName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASPACKAGE)]
        public string PackageDescription;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASPACKAGE)]
        public string PackageCreatedBy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASPACKAGE)]
        public string PackageSourceEnvironment;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASPACKAGE)]
        public string OutPath;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASPACKAGE)]
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
                    if (!Force && !ShouldContinue($"File '{OutPath}' exists. Overwrite?", "Export Flow"))
                    {
                        // Exit cmdlet
                        return;
                    }
                }
            }

            var environmentName = Environment.GetName();
            var flowName = Identity.GetName();

            if (AsZipPackage)
            {
                var postData = new
                {
                    baseResourceIds = new[] {
                    $"/providers/Microsoft.Flow/flows/{flowName}"
                }
                };
                var wrapper = RestHelper.PostAsync<Model.PowerPlatform.PowerAutomate.FlowExportPackageWrapper>(Connection.HttpClient, $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/listPackageResources?api-version=2016-11-01", AccessToken, payload: postData).GetAwaiter().GetResult();

                if (wrapper.Status == Model.PowerPlatform.PowerAutomate.Enums.FlowExportStatus.Succeeded)
                {
                    foreach (var resource in wrapper.Resources)
                    {
                        if (resource.Value.Type == "Microsoft.Flow/flows")
                        {
                            resource.Value.SuggestedCreationType = "Update";
                        }
                        else
                        {
                            resource.Value.SuggestedCreationType = "Existing";
                        }
                    }
                    dynamic details = new System.Dynamic.ExpandoObject();

                    var exportPostData = new
                    {
                        includedResourceIds = new[]
                        {
                             $"/providers/Microsoft.Flow/flows/{flowName}"
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

                    var resultElement = RestHelper.PostAsync<JsonElement>(Connection.HttpClient, $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/exportPackage?api-version=2016-11-01", AccessToken, payload: exportPostData).GetAwaiter().GetResult();
                    if (resultElement.TryGetProperty("status", out JsonElement statusElement))
                    {
                        if (statusElement.GetString() == "Succeeded")
                        {
                            if (resultElement.TryGetProperty("packageLink", out JsonElement packageLinkElement))
                            {
                                if (packageLinkElement.TryGetProperty("value", out JsonElement valueElement))
                                {
                                    var packageLink = valueElement.GetString();
                                    using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, packageLink))
                                    {
                                        requestMessage.Version = new Version(2, 0);
                                        //requestMessage.Headers.Add("Authorization", $"Bearer {AccessToken}");
                                        var response = Connection.HttpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
                                        var byteArray = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
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
                            }
                        }
                    }
                }
                else
                {
                    // Errors have been reported in the export request result
                    foreach (var error in wrapper.Errors)
                    {
                        WriteVerbose($"Export failed for {flowName} with error {error.Code}: {error.Message}");
                    }
                }
            }
            else
            {
                var json = RestHelper.PostAsync(Connection.HttpClient, $"https://api.flow.microsoft.com/providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/exportToARMTemplate?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                WriteObject(json);
            }
        }
    }
}