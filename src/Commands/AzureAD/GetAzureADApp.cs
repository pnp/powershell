using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADApp", DefaultParameterSetName = ParameterSet_Identity)]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Application.Read.All")]
    [Alias("Get-PnPEntraIDApp")]
    public class GetAzureADApp : PnPGraphCmdlet
    {
        private const string ParameterSet_Identity = "Identity";
        private const string ParameterSet_Filter = "Filter";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Identity)]
        public AzureADAppPipeBind Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_Filter)]
        public string Filter = null;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Identity.GetApp(GraphRequestHelper));
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
                var result = GraphRequestHelper.GetResultCollection<AzureADApp>(requestUrl, additionalHeaders: additionalHeaders);
                WriteObject(result, true);
            }
        }
    }
}