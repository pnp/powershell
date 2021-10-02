﻿using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.REST;
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
            var environments = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.Environment.Environment>(HttpClient, "https://management.azure.com/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();           
            WriteObject(environments, true);
        }
    }
}