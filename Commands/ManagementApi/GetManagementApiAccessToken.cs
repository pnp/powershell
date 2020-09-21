using System;
using System.Management.Automation;
using PnP.PowerShell.CmdletHelpAttributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Model;

namespace PnP.PowerShell.Commands.ManagementApi
{
    [Cmdlet(VerbsCommon.Get, "PnPManagementApiAccessToken")]
    [Obsolete("Connect using Connect-PnPOnline -ClientId -ClientSecret -AADDomain instead to set up a connection with which Office 365 Management API cmdlets can be executed")]
    public class GetManagementApiAccessToken : BasePSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string TenantId;

        [Parameter(Mandatory = true)]
        public string ClientId;

        [Parameter(Mandatory = true)]
        public string ClientSecret;

        protected override void ExecuteCmdlet()
        {
            var officeManagementApiToken = OfficeManagementApiToken.AcquireApplicationToken(TenantId, ClientId, ClientSecret, PnPConnection.CurrentConnection.AzureEnvironment);
            WriteObject(officeManagementApiToken.AccessToken);
        }
    }
}