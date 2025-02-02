
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Get, "PnPWebTemplates")]
    public class GetWebTemplates : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = false)]
        public uint Lcid;

        [Parameter(Mandatory = false)]
        public int CompatibilityLevel;

        protected override void ProcessRecord()
        {
            WriteObject(Tenant.GetWebTemplates(Lcid, CompatibilityLevel),true);
        }
    }
}