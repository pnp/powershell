using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Enums;
using Microsoft.SharePoint.Client;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.BusinessData.MetadataModel;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPFlow")]
    public class GetFlow : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public PowerAutomateFlowPipeBind Identity;

        [Parameter(Mandatory = false)]
        public FlowSharingStatus SharingStatus = FlowSharingStatus.All;

        protected override void ExecuteCmdlet()
        {
            bool sharingStatus = false;
            string flowSharing = null;
            switch (SharingStatus)
            {
                case FlowSharingStatus.SharedWithMe:
                    flowSharing = "&$filter = search('team')";
                    sharingStatus = true;
                    break;

                case FlowSharingStatus.Personal:
                    flowSharing = "&$filter=search('personal')";
                    sharingStatus = true;
                    break;

                case FlowSharingStatus.All:
                    flowSharing = "&$filter=search('personal')&$filter = search('team')";
                    sharingStatus = true;
                    break;
            }

            string environmentName = null;
            if(ParameterSpecified(nameof(Environment)))
            {
                environmentName = Environment.GetName();

                WriteVerbose($"Using environment as provided '{environmentName}'");
            }
            else
            {
                var environments = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.Environment.Environment>(Connection, "https://management.azure.com/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                environmentName = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Name;

                if(string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                WriteVerbose($"Using default environment as retrieved '{environmentName}'");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var flowName = Identity.GetName();

                WriteVerbose($"Retrieving specific Power Automate Flow with the provided name '{flowName}' within the environment '{environmentName}'");

                var result = GraphHelper.GetAsync<Model.PowerPlatform.PowerAutomate.Flow>(Connection, sharingStatus ? $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}?api-version=2016-11-01" + flowSharing : $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                WriteObject(result, false);
            }
            else
            {
                WriteVerbose($"Retrieving all Power Automate Flows within environment '{environmentName}'");

                var flows = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.PowerAutomate.Flow>(Connection, sharingStatus ? $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows?api-version=2016-11-01" + flowSharing : $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                WriteObject(flows, true);

            }
        }
    }
}