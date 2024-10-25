using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Management.Automation;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Attributes;
using System;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPFlow", DefaultParameterSetName = ParameterSet_ALL)]
    [ApiNotAvailableUnderApplicationPermissions]
    [RequiredApiDelegatedPermissions("azure/user_impersonation")]
    public class GetFlow : PnPAzureManagementApiCmdlet
    {
        private const string ParameterSet_BYIDENTITY = "By Identity";
        private const string ParameterSet_ALL = "All";

        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_BYIDENTITY)]
        [Parameter(Mandatory = false, ValueFromPipeline = true, ParameterSetName = ParameterSet_ALL)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYIDENTITY)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYIDENTITY)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_ALL)]
        public FlowSharingStatus SharingStatus = FlowSharingStatus.All;

        protected override void ExecuteCmdlet()
        {
            try
            {
                var environmentName = ParameterSpecified(nameof(Environment)) ? Environment.GetName() : PowerPlatformUtility.GetDefaultEnvironment(this, Connection, Connection.AzureEnvironment, AccessToken)?.Name;
                string baseUrl = PowerPlatformUtility.GetPowerAutomateEndpoint(Connection.AzureEnvironment);

                if (ParameterSpecified(nameof(Identity)))
                {
                    var flowName = Identity.GetName();

                    WriteVerbose($"Retrieving specific Power Automate Flow with the provided name '{flowName}' within the environment '{environmentName}'");

                    var result = GraphHelper.Get<Model.PowerPlatform.PowerAutomate.Flow>(this, Connection, baseUrl + $"/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}?api-version=2016-11-01", AccessToken);
                    WriteObject(result, false);
                }
                else
                {
                    string filter = null;
                    switch (SharingStatus)
                    {
                        case FlowSharingStatus.SharedWithMe:
                            filter = "search('team')";
                            break;

                        case FlowSharingStatus.Personal:
                            filter = "search('personal')";
                            break;

                        case FlowSharingStatus.All:
                            filter = "search('team AND personal')";
                            break;
                    }

                    WriteVerbose($"Retrieving all Power Automate Flows within environment '{environmentName}'{(filter != null ? $" with filter '{filter}'" : "")}");

                    var flowUrl = $"{baseUrl}/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/{(AsAdmin ? "v2" : "")}/flows?api-version=2016-11-01{(filter != null ? $"&$filter={filter}" : "")}";
                    var flows = GraphHelper.GetResultCollection<Model.PowerPlatform.PowerAutomate.Flow>(this, Connection, flowUrl, AccessToken);

                    WriteObject(flows, true);

                }
            }
            catch (Exception e)
            {
                WriteError(new ErrorRecord(new Exception("Make sure you have granted access to Azure AD App to Interact with Power Platform, To help understand the required permissions visit https://pnp.github.io/powershell/articles/determinepermissions.html#help-i-cant-figure-out-which-permissions-i-need"), e.Message, ErrorCategory.AuthenticationError, null));
            }
        }
    }
}
