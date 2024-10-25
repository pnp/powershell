using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsData.Export, "PnPPowerApp")]
    [RequiredApiApplicationPermissions("https://management.azure.com/.default")]
    public class ExportPowerApp : PnPAzureManagementApiCmdlet
    {
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

            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(this, Connection, Connection.AzureEnvironment, AccessToken)?.Name;
            var appName = Identity.GetName();

            var wrapper = PowerAppsUtility.GetWrapper(Connection.HttpClient, environmentName, AccessToken, appName, Connection.AzureEnvironment);

            if (wrapper.Status == Model.PowerPlatform.PowerApp.Enums.PowerAppExportStatus.Succeeded)
            {
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
                var responseHeader = PowerAppsUtility.GetResponseHeader(Connection.HttpClient, environmentName, AccessToken, appName, wrapper, objectDetails);


                var packageLink = PowerAppsUtility.GetPackageLink(Connection.HttpClient, Convert.ToString(responseHeader.Location), AccessToken);
                var getFileByteArray = PowerAppsUtility.GetFileByteArray(Connection.HttpClient, packageLink, AccessToken);
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

                System.IO.File.WriteAllBytes(fileName, getFileByteArray);
                var returnObject = new PSObject();
                returnObject.Properties.Add(new PSNoteProperty("Filename", fileName));
                WriteObject(returnObject);
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

    }
}
