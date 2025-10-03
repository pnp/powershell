using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;


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
            var importStatus = ImportFlowUtility.ExecuteImportFlow(Connection.HttpClient,AccessToken,baseUrl,environmentName,PackagePath,Name);
            WriteObject(importStatus);
        }

        private string GetEnvironmentName()
        {
            return ParameterSpecified(nameof(Environment))
                ? Environment.GetName()
                : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
        }
    }
}
