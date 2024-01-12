﻿using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerApp")]
    [OutputType(typeof(Model.PowerPlatform.PowerApp.PowerApp))]
    public class GetPowerApp : PnPAzureManagementApiCmdlet
    {
        [Parameter(Mandatory = false, ValueFromPipeline = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public PowerAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            string environmentName = null;
            if(ParameterSpecified(nameof(Environment)))
            {
                environmentName = Environment.GetName();

                WriteVerbose($"Using environment as provided '{environmentName}'");
            }
            else
            {
                string baseUrl = "https://api.flow.microsoft.com/";
                var environments = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.Environment.Environment>(Connection, baseUrl + "/providers/Microsoft.ProcessSimple/environments?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                environmentName = environments.FirstOrDefault(e => e.Properties.IsDefault.HasValue && e.Properties.IsDefault == true)?.Name;

                if(string.IsNullOrEmpty(environmentName))
                {
                    throw new Exception($"No default environment found, please pass in a specific environment name using the {nameof(Environment)} parameter");
                }

                WriteVerbose($"Using default environment as retrieved '{environmentName}'");
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                var appName = Identity.GetName();

                WriteVerbose($"Retrieving specific PowerApp with the provided name '{appName}' within the environment '{environmentName}'");

                var result = GraphHelper.GetAsync<Model.PowerPlatform.PowerApp.PowerApp>(Connection, $"https://api.powerapps.com/providers/Microsoft.PowerApps{(AsAdmin ? "/scopes/admin/environments/" + environmentName : "")}/apps/{appName}?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                 
                WriteObject(result, false);
            }
            else
            {
                WriteVerbose($"Retrieving all PowerApps within environment '{environmentName}'");

                var apps = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.PowerApp.PowerApp>(Connection, $"https://api.powerapps.com/providers/Microsoft.PowerApps/apps?api-version=2016-11-01&$filter=environment eq '{environmentName}'", AccessToken).GetAwaiter().GetResult();
                WriteObject(apps, true);
            }
        }
    }
}