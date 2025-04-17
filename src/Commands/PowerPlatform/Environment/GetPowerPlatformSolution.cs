using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Management.Automation;
using System.Linq;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Attributes;

namespace PnP.PowerShell.Commands.PowerPlatform.Environment
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerPlatformSolution")]
    [RequiredApiDelegatedPermissions("azure/user_impersonation")]
    public class GetPowerPlatformSolution: PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public PowerPlatformSolutionPipeBind Name;

        protected override void ExecuteCmdlet()
        {
           
            var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(ArmRequestHelper, Connection.AzureEnvironment)?.Name;
            string dynamicsScopeUrl = null;
            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            var environments = ArmRequestHelper.GetResultCollection<Model.PowerPlatform.Environment.Environment>( $"{baseUrl}/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01");
            if (ParameterSpecified(nameof(Environment)))
            {
                LogDebug($"Using environment as provided '{environmentName}'");
                dynamicsScopeUrl = environments.FirstOrDefault(e => e.Properties.DisplayName.ToLower() == environmentName || e.Name.ToLower() == environmentName)?.Properties.LinkedEnvironmentMetadata.InstanceApiUrl;
            }
            else
            {
                dynamicsScopeUrl = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Properties.LinkedEnvironmentMetadata.InstanceApiUrl;
                if (string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                LogDebug($"Using default environment as retrieved '{environmentName}'");
            }

           // string accessTokenForGettingSolutions = TokenHandler.GetAccessToken(this, $"{dynamicsScopeUrl}/.default", Connection);

            var dynamicRequestHelper = new ApiRequestHelper(GetType(),this.Connection,$"{dynamicsScopeUrl}/.default");

            if (ParameterSpecified(nameof(Name)))
            {
                var solutionName = Name.GetName();

                LogDebug($"Retrieving specific solution with the provided name '{solutionName}' within the environment '{environmentName}'");

                var requestUrl = dynamicsScopeUrl + "/api/data/v9.0/solutions?$filter=isvisible eq true and friendlyname eq '" + solutionName + "'&$expand=publisherid&api-version=9.1";
                var solution = dynamicRequestHelper.GetResultCollection<Model.PowerPlatform.Environment.Solution.PowerPlatformSolution>(requestUrl);
                WriteObject(solution, false);
            }
            else
            {
                LogDebug($"Retrieving all Solutions within environment '{environmentName}'");
                var requestUrl = dynamicsScopeUrl + "/api/data/v9.0/solutions?$filter=isvisible eq true&$expand=publisherid($select=friendlyname)&api-version=9.1";
                var solutions = dynamicRequestHelper.GetResultCollection<Model.PowerPlatform.Environment.Solution.PowerPlatformSolution>(requestUrl);
                WriteObject(solutions, true);
            }
        }
    }
}