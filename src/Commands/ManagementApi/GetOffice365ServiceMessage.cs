using System;
using System.Management.Automation;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;
using PnP.PowerShell.Commands.Utilities.REST;

namespace PnP.PowerShell.Commands.ManagementApi
{
    [Cmdlet(VerbsCommon.Get, "PnPOffice365ServiceMessage")]
    [RequiredMinimalApiPermissions("https://manage.office.com/ServiceHealth.Read")]
    [Obsolete("Use Get-PnPMessageCenterAnnouncement instead. It uses the Microsoft Graph backend which returns slightly different data. The Office Management API used by this cmdlet will be pulled by Microsoft in the future.")]
    public class GetOffice365ServiceMessage : PnPOfficeManagementApiCmdlet
    {
        [Parameter(Mandatory = false)]
        public Enums.Office365Workload? Workload;

        protected override void ExecuteCmdlet()
        {
            var collection = GraphHelper.GetResultCollectionAsync<ManagementApiServiceMessage>(HttpClient, $"{ApiRootUrl}ServiceComms/Messages{(ParameterSpecified(nameof(Workload)) ? $"?$filter=Workload eq '{Workload.Value}'" : "")}", AccessToken, false).GetAwaiter().GetResult();
            WriteObject(collection, true);
        }
    }
}