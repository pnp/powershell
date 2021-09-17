using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.ManagementApi
{
    [Cmdlet(VerbsCommon.Get, "PnPOffice365HistoricalServiceStatus")]
    [RequiredMinimalApiPermissions("https://manage.office.com/ServiceHealth.Read")]
    [Obsolete("Use Get-PnPServiceHealthIssue instead. It uses the Microsoft Graph backend which returns slightly different data. The Office Management API used by this cmdlet will be pulled by Microsoft in the future.")]
    public class GetOffice365HistoricalServiceStatus : PnPOfficeManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public Enums.Office365Workload? Workload;

        protected override void ExecuteCmdlet()
        {
            var collection = GraphHelper.GetAsync<RestResultCollection<ManagementApiServiceStatus>>(HttpClient, $"{ApiRootUrl}ServiceComms/HistoricalStatus{(ParameterSpecified(nameof(Workload)) ? $"?$filter=Workload eq '{Workload.Value}'" : "")}", AccessToken, false).GetAwaiter().GetResult();

            if (collection != null)
            {
                WriteObject(collection.Items, true);
            }
        }
    }
}