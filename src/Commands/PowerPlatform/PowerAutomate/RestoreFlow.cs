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
        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = true)]
        public PowerAutomateFlowPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            var environmentName = Environment.GetName();
            var flowName = Identity.GetName();

            WriteVerbose($"Attempting to restore Flow with name {flowName}");

            try
            {
                WriteVerbose($"Restoring soft-deleted flow  {flowName} from environment ${environmentName}");
                RestHelper.Post(Connection.HttpClient, $"{baseUrl}/providers/Microsoft.ProcessSimple/scopes/admin/environments/{environmentName}/flows/{flowName}/restore?api-version=2016-11-01", AccessToken);
                WriteVerbose($"Flow with name {flowName} restored");
            }
            catch
            {

                throw new Exception($"Failed to restore flow with name {flowName}");
            }
        }

    }
}