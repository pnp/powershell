using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.Environment
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerPlatformEnvironment")]
    [Alias("Get-PnPFlowEnvironment")]
    [WriteAliasWarning("Get-PnPFlowEnvironment has been replaced by Get-PnPPowerPlatformEnvironment. It will be removed in a future version. Please update your scripts.")]
    [RequiredMinimalApiPermissions("https://management.azure.com/.default")]
    public class GetPowerPlatformEnvironment : PnPGraphCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            List<Model.PowerPlatform.Environment.Environment> environments = new List<Model.PowerPlatform.Environment.Environment>();
            var result = RestHelper.GetAsync<RestResultCollection<Model.PowerPlatform.Environment.Environment>>(HttpClient, "https://management.azure.com/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
            if (result.Items.Any())
            {
                environments.AddRange(result.Items);
                while (!string.IsNullOrEmpty(result.NextLink))
                {
                    result = RestHelper.GetAsync<RestResultCollection<Model.PowerPlatform.Environment.Environment>>(HttpClient, result.NextLink, AccessToken).GetAwaiter().GetResult();
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