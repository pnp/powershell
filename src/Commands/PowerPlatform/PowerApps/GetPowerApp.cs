using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management.Automation;


namespace PnP.PowerShell.Commands.PowerPlatform.PowerApps
{
    [Cmdlet(VerbsCommon.Get, "PnPPowerApp")]
    [RequiredMinimalApiPermissions("https://management.azure.com/.default")]
    public class GetPowerApp : PnPGraphCmdlet
    {

        [Parameter(Mandatory = true)]
        public PowerPlatformEnvironmentPipeBind Environment;

        [Parameter(Mandatory = false)]
        public SwitchParameter AsAdmin;

        [Parameter(Mandatory = false)]
        public PowerAppPipeBind Identity;

        protected override void ExecuteCmdlet()
        {
            var environmentName = Environment.GetName();

            if (ParameterSpecified(nameof(Identity)))
            {
                var appName = Identity.GetName();
                var result = GraphHelper.GetAsync<Model.PowerPlatform.PowerApp.PowerApp>(Connection, $"https://api.powerapps.com/providers/Microsoft.PowerApps{(AsAdmin ? "/scopes/admin/environments/" + environmentName : "")}/apps/{appName}?api-version=2016-11-01", AccessToken).GetAwaiter().GetResult();
                 
                WriteObject(result, false);
            }
            else
            {
                var apps = GraphHelper.GetResultCollectionAsync<Model.PowerPlatform.PowerApp.PowerApp>(Connection, $"https://api.powerapps.com/providers/Microsoft.PowerApps/apps?api-version=2016-11-01&$filter=environment eq '{environmentName}'", AccessToken).GetAwaiter().GetResult();
                WriteObject(apps, true);
            }
        }

    }
}
