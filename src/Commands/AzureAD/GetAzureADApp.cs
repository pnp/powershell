using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.AzureAD
{
    [Cmdlet(VerbsCommon.Get, "PnPAzureADApp")]
    [RequiredMinimalApiPermissions("Application.Read.All")]
    public class GetAzureADApp : PnPGraphCmdlet
    {
        [Parameter(Mandatory = false)]
        public AzureADAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                WriteObject(Identity.GetApp(this, Connection, AccessToken));
            }
            else
            {
                List<AzureADApp> apps = new List<AzureADApp>();
                var result = GraphHelper.GetResultCollectionAsync<AzureADApp>(Connection, "/v1.0/applications", AccessToken).GetAwaiter().GetResult();
                WriteObject(result, true);
            }
        }
    }
}