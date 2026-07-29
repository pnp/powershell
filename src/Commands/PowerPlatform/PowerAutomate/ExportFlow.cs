using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;
using System.Text.Json;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsData.Export, "PnPFlow")]
    [RequiredApiDelegatedPermissions("azure/user_impersonation", "https://service.powerapps.com/user")]
    public class ExportFlow : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_ASJSON = "As Json";
        private const string ParameterSet_ASPACKAGE = "As ZIP Package";
        private const string ExportFlowFailedErrorId = "ExportPnPFlowFailed";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASPACKAGE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ASJSON)]
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
                    if (!Force && !ShouldContinue($"File '{OutPath}' exists. Overwrite?", Properties.Resources.Confirm))
                    {
                        return;
                    }
                }
            }

            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            var flowName = Identity.GetName();

            if (AsZipPackage)
            {
                PowerPlatformExportUtility.RunExport(() => ExportAsZipPackage(environmentName, flowName), details => WriteExportError(flowName, details));
            }
            else
            {
                string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
                var json = RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.ProcessSimple/environments/{environmentName}/flows/{flowName}/exportToARMTemplate?api-version=2016-11-01", AccessToken);
                WriteObject(json);
            }
        }

        private void ExportAsZipPackage(string environmentName, string flowName)
        {
            var powerAppsServiceAccessToken = PowerAppsServiceAccessToken;
            var postData = new
            {
                baseResourceIds = new[] {
                    $"/providers/Microsoft.Flow/flows/{flowName}"
                }
            };
            string baseUrl = PowerPlatformUtility.GetBapEndpoint(Connection.AzureEnvironment);
            var wrapper = RestHelper.Post<Model.PowerPlatform.PowerAutomate.FlowExportPackageWrapper>(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/listPackageResources?api-version=2016-11-01", powerAppsServiceAccessToken, payload: postData);

            if (wrapper == null)
            {
                WriteExportError(flowName, "The service returned an unexpected response when listing the package resources.");
                return;
            }

            if (wrapper.Status != Model.PowerPlatform.PowerAutomate.Enums.FlowExportStatus.Succeeded)
            {
                WriteExportError(flowName, PowerPlatformExportUtility.DescribeFailure(wrapper.Errors?.Where(error => error != null).Select(error => (error.Code, error.Message)), wrapper.StatusRaw));
                return;
            }

            if (wrapper.Resources == null || wrapper.Resources.Count == 0)
            {
                WriteExportError(flowName, "The service returned a successful status without any package resources.");
                return;
            }

            if (wrapper.Resources.Any(resource => resource.Value == null))
            {
                WriteExportError(flowName, "The service returned a package resource without its details.");
                return;
            }

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

            var resultElement = RestHelper.Post<JsonElement>(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.BusinessAppPlatform/environments/{environmentName}/exportPackage?api-version=2016-11-01", powerAppsServiceAccessToken, payload: exportPostData);
            if (resultElement.ValueKind != JsonValueKind.Object)
            {
                WriteExportError(flowName, "The service returned an unexpected response when creating the export package.");
                return;
            }

            var exportStatus = PowerPlatformExportUtility.GetStringProperty(resultElement, "status");
            if (!string.Equals(exportStatus, "Succeeded", StringComparison.OrdinalIgnoreCase))
            {
                WriteExportError(flowName, PowerPlatformExportUtility.DescribeFailure(resultElement, exportStatus));
                return;
            }

            if (!resultElement.TryGetProperty("packageLink", out JsonElement packageLinkElement) || packageLinkElement.ValueKind != JsonValueKind.Object)
            {
                WriteExportError(flowName, "The service returned a successful status without a package link.");
                return;
            }

            var packageLink = PowerPlatformExportUtility.GetStringProperty(packageLinkElement, "value");
            if (string.IsNullOrWhiteSpace(packageLink))
            {
                WriteExportError(flowName, "The service returned a successful status with an empty package link.");
                return;
            }

            string fileName;
            if (ParameterSpecified(nameof(OutPath)))
            {
                fileName = OutPath;
            }
            else
            {
                fileName = PowerPlatformExportUtility.GetFileNameFromPackageLink(packageLink);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    WriteExportError(flowName, "The package link did not contain a ZIP filename. Specify -OutPath to provide a filename.");
                    return;
                }
                fileName = System.IO.Path.Combine(SessionState.Path.CurrentFileSystemLocation.Path, fileName);

                // The filename is only known once the service returned the package link, so unlike with OutPath the
                // confirmation to overwrite an existing file can only be asked for here
                if (System.IO.File.Exists(fileName) && !Force && !ShouldContinue($"File '{fileName}' exists. Overwrite?", Properties.Resources.Confirm))
                {
                    return;
                }
            }

            var byteArray = RestHelper.GetByteArray(Connection.HttpClient, packageLink, null, "application/zip");
            System.IO.File.WriteAllBytes(fileName, byteArray);
            var returnObject = new PSObject();
            returnObject.Properties.Add(new PSNoteProperty("Filename", fileName));
            WriteObject(returnObject);
        }

        private void WriteExportError(string flowName, string details)
        {
            var message = $"Export failed for flow '{flowName}': {details}";
            WriteError(new ErrorRecord(new PSInvalidOperationException(message), ExportFlowFailedErrorId, ErrorCategory.InvalidOperation, flowName));
        }
    }
}
