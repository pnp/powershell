using PnP.Framework.Entities;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPFlowEnvironment")]
    [RequiredMinimalApiPermissions("https://management.azure.com/.default")]
    public class GetFlowEnvironment : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            List<Model.PowerAutomate.Environment> environments = new List<Model.PowerAutomate.Environment>();
            var result = RestHelper.GetAsync<RestResultCollection<Model.PowerAutomate.Environment>>(HttpClient, "https://management.azure.com/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
            if (result.Items.Any())
            {
                environments.AddRange(result.Items);
                while (!string.IsNullOrEmpty(result.NextLink))
                {
                    result = RestHelper.GetAsync<RestResultCollection<Model.PowerAutomate.Environment>>(HttpClient, result.NextLink, AccessToken).GetAwaiter().GetResult();
                    if (result.Items.Any())
                    {
                        environments.AddRange(result.Items);
                    }
                }
            }
            WriteObject(environments, true);
        }
    }
}