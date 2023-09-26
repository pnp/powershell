using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.AzureAD;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.EntraID
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDApp", DefaultParameterSetName = ParameterSet_Identity)]
    [RequiredMinimalApiPermissions("Application.Read.All")]
    [Alias("Get-PnPAzureADApp")]
    public class GetEntraIDApp : PnPGraphCmdlet
    {
        private const string ParameterSet_Identity = "Identity";
        private const string ParameterSet_Filter = "Filter";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Identity)]
        public EntraIDAppPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Filter)]
        public string Filter = null;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Identity.GetApp(this, Connection, AccessToken));
            }
            else
            {
                Dictionary<string, string> additionalHeaders = null;
                string requestUrl = "/v1.0/applications";
                if (!string.IsNullOrEmpty(Filter))
                {
                    requestUrl = $"{requestUrl}?$filter=({Filter})";

                    additionalHeaders = new Dictionary<string, string>
                    {
                        { "ConsistencyLevel", "eventual" }
                    };
                }
                var result = GraphHelper.GetResultCollectionAsync<App>(Connection, requestUrl, AccessToken, additionalHeaders: additionalHeaders).GetAwaiter().GetResult();
                WriteObject(result, true);
            }
        }
    }
}