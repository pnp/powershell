using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsData.Restore, "PnPFlow")]
    public class RestoreFlow : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);

            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            var flowName = Identity.GetName();
            
            LogDebug($"Restoring soft-deleted flow {flowName} from environment {environmentName}");

            try
            {
                RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environmentName}/flows/{flowName}/restore?api-version=2016-11-01", AccessToken);
                LogDebug($"Flow with name {flowName} restored");
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to restore flow with name {flowName} from environment {environmentName} with exception: {ex.Message}", ex);
            }
        }
    }
}