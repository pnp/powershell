using System.Collections.Generic;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.ManagementApi
{
    [Cmdlet(VerbsCommon.Get, "Office365ServiceMessage")]
    [OfficeManagementApiPermissionCheck(OfficeManagementApiPermission.ServiceHealth_Read)]
    [PnPManagementShellScopes("ServiceHealth.Read")]
    public class GetOffice365ServiceMessage : PnPOfficeManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public Enums.Office365Workload? Workload;

        protected override void ExecuteCmdlet()
        {
            var collection = GraphHelper.GetAsync<GraphCollection<ManagementApiServiceMessage>>(HttpClient, $"{ApiRootUrl}ServiceComms/Messages{(ParameterSpecified(nameof(Workload)) ? $"?$filter=Workload eq '{Workload.Value}'" : "")}", AccessToken, false).GetAwaiter().GetResult();

            if (collection != null)
            {
                WriteObject(collection.Items, true);
            }
        }
    }
}