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
            string environmentName = null;
            string dynamicsScopeUrl = null;
            string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);
            var environments = GraphHelper.GetResultCollection<Model.PowerPlatform.Environment.Environment>(this, Connection, $"{baseUrl}/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken);
            if (ParameterSpecified(nameof(Environment)))
            {
                environmentName = Environment.GetName().ToLower();
                WriteVerbose($"Using environment as provided '{environmentName}'");
                dynamicsScopeUrl = environments.FirstOrDefault(e => e.Properties.DisplayName.ToLower() == environmentName || e.Name.ToLower() == environmentName)?.Properties.LinkedEnvironmentMetadata.InstanceApiUrl;
            }
            else
            {
                environmentName = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Name;
                dynamicsScopeUrl = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Properties.LinkedEnvironmentMetadata.InstanceApiUrl;
                if (string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                WriteVerbose($"Using default environment as retrieved '{environmentName}'");
            }

            string accessTokenForGettingSolutions = TokenHandler.GetAccessToken(this, $"{dynamicsScopeUrl}/.default", Connection);

            if (ParameterSpecified(nameof(Name)))
            {
                var solutionName = Name.GetName();

                WriteVerbose($"Retrieving specific solution with the provided name '{solutionName}' within the environment '{environmentName}'");

                var requestUrl = dynamicsScopeUrl + "/api/data/v9.0/solutions?$filter=isvisible eq true and friendlyname eq '" + solutionName + "'&$expand=publisherid&api-version=9.1";
                var solution = GraphHelper.GetResultCollection<Model.PowerPlatform.Environment.Solution.PowerPlatformSolution>(this, Connection, requestUrl, accessTokenForGettingSolutions);
                WriteObject(solution, false);
            }
            else
            {
                WriteVerbose($"Retrieving all Solutions within environment '{environmentName}'");
                var requestUrl = dynamicsScopeUrl + "/api/data/v9.0/solutions?$filter=isvisible eq true&$expand=publisherid($select=friendlyname)&api-version=9.1";
                var solutions = GraphHelper.GetResultCollection<Model.PowerPlatform.Environment.Solution.PowerPlatformSolution>(this, Connection, requestUrl, accessTokenForGettingSolutions);
                WriteObject(solutions, true);
            }
        }
    }
}