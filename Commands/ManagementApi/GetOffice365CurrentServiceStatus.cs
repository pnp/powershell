using System.Collections.Generic;
using System.Management.Automation;
using System.Net.Http;
using PnP.Framework.Graph;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.ManagementApi
{
    [Cmdlet(VerbsCommon.Get, "Office365CurrentServiceStatus")]
    [OfficeManagementApiPermissionCheck(OfficeManagementApiPermission.ServiceHealth_Read)]
    [PnPManagementShellScopes("ServiceHealth.Read")]
    public class GetOffice365CurrentServiceStatus : PnPOfficeManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public Enums.Office365Workload? Workload;

        protected override void ExecuteCmdlet()
        {
            var collection = GraphHelper.GetAsync<GraphCollection<ManagementApiServiceStatus>>(HttpClient, $"{ApiRootUrl}ServiceComms/CurrentStatus{(ParameterSpecified(nameof(Workload)) ? $"?$filter=Workload eq '{Workload.Value}'" : "")}", AccessToken, false).GetAwaiter().GetResult();

            if (collection != null)
            {
                WriteObject(collection.Items, true);
            }
        }
    }
}