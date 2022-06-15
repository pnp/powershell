using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerAutomate
{
    [Cmdlet(VerbsCommon.Get, "PnPFlow")]
    [RequiredMinimalApiPermissions("https://management.azure.com/.default")]
    public class GetFlow : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public PowerAutomateFlowPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var environmentName = Environment.GetName();

            if (ParameterSpecified(nameof(Identity)))
            {
                var flowName = Identity.GetName();
                var result = GraphHelper.GetAsync<Model.PowerPlatform.PowerAutomate.Flow>(Connection, $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows/{flowName}?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                WriteObject(result, false);
            }
            else
            {
                var flows = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.PowerAutomate.Flow>(Connection, $"https://management.azure.com/providers/Microsoft.ProcessSimple{(AsAdmin ? "/scopes/admin" : "")}/environments/{environmentName}/flows?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                WriteObject(flows, true);
            }
        }
    }
}