using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsData.Export, "PnPPowerApp")]
    [RequiredApiApplicationPermissions("https://management.azure.com/user_impersonation", "https://service.powerapps.com/user")]
    [RequiredApiDelegatedPermissions("azure/user_impersonation", "https://service.powerapps.com/user")]
    public class ExportPowerApp : PnPAzureManagementApiCmdlet
    {
        private const string ExportPowerAppFailedErrorId = "ExportPnPPowerAppFailed";

        [Parameter(Mandatory = false)]
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
                    if (!Force && !ShouldContinue($"File '{OutPath}' exists. Overwrite?", Properties.Resources.Confirm))
                    {
                        // Exit cmdlet
                        return;
                    }
                }
            }

            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            var appName = Identity.GetName();
            PowerPlatformExportUtility.RunExport(() => ExportPackage(environmentName, appName), details => WriteExportError(appName, details));
        }

        private void ExportPackage(string environmentName, string appName)
        {
            var powerAppsServiceAccessToken = PowerAppsServiceAccessToken;
            var wrapper = PowerAppsUtility.GetWrapper(Connection.HttpClient, environmentName, powerAppsServiceAccessToken, appName, Connection.AzureEnvironment);

            if (wrapper == null)
            {
                WriteExportError(appName, "The service returned an unexpected response when listing the package resources.");
                return;
            }

            if (wrapper.Status != Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Succeeded)
            {
                WriteExportError(appName, PowerPlatformExportUtility.DescribeFailure(wrapper.Errors?.Where(error => error != null).Select(error => (error.Code, error.Message)), wrapper.StatusRaw));
                return;
            }

            if (wrapper.Resources == null || wrapper.Resources.Count == 0 || wrapper.Resources.Any(resource => resource.Value == null))
            {
                WriteExportError(appName, "The service returned a successful status without valid package resources.");
                return;
            }

            foreach (var resource in wrapper.Resources)
            {
                if (resource.Value.Type == "Microsoft.PowerApps/apps")
                {
                    resource.Value.SuggestedCreationType = "Update";
                }
            }

            var objectDetails = new
            {
                displayName = PackageDisplayName,
                description = PackageDescription,
                creator = PackageCreatedBy,
                sourceEnvironment = PackageSourceEnvironment
            };
            var responseLocation = PowerAppsUtility.GetResponseLocation(Connection.HttpClient, environmentName, powerAppsServiceAccessToken, appName, wrapper, objectDetails, Connection.AzureEnvironment);
            var packageLink = PowerAppsUtility.GetPackageLink(Connection.HttpClient, Convert.ToString(responseLocation), powerAppsServiceAccessToken);

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
                    WriteExportError(appName, "The package link did not contain a ZIP filename. Specify -OutPath to provide a filename.");
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

            var fileBytes = Utilities.REST.RestHelper.GetByteArray(Connection.HttpClient, packageLink, null, "application/zip");
            System.IO.File.WriteAllBytes(fileName, fileBytes);
            var returnObject = new PSObject();
            returnObject.Properties.Add(new PSNoteProperty("Filename", fileName));
            WriteObject(returnObject);
        }

        private void WriteExportError(string appName, string details)
        {
            var message = $"Export failed for Power App '{appName}': {details}";
            WriteError(new ErrorRecord(new PSInvalidOperationException(message), ExportPowerAppFailedErrorId, ErrorCategory.InvalidOperation, appName));
        }

    }
}
