using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;

using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPSiteUserInvitations")]
    public class GetSiteUserInvitations : PnPAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public SitePipeBind Site;

        [Parameter(Mandatory = true)]
        public string EmailAddress;

        protected override void ExecuteCmdlet()
        {
            var url = Connection.Url;
            if(ParameterSpecified(nameof(Site)))
            {
                url = Site.Url;
            }
            var invitations = Tenant.GetSPOTenantSiteUserInvitations(url, EmailAddress);
            ClientContext.Load(invitations);
            ClientContext.ExecuteQueryRetry();
            WriteObject(invitations,true);
            
        }
    }
}