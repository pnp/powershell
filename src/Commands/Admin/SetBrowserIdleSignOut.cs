using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPBrowserIdleSignout")]
    public class SetBrowserIdleSignout : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        public bool Enabled;

        [Parameter(Mandatory = false)]
        public TimeSpan WarnAfter;

        [Parameter(Mandatory = false)]
        public TimeSpan SignoutAfter;

        protected override void ExecuteCmdlet()
        {
            var result = this.Tenant.SetIdleSessionSignOutForUnmanagedDevices(Enabled,WarnAfter,SignoutAfter);
            ClientContext.ExecuteQueryRetry();
        }
    }


}