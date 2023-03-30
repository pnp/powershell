using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Set, "PnPBrowserIdleSignout", DefaultParameterSetName = DisableBrowserIdleSignout)]
    public class SetBrowserIdleSignout : PnPAdminCmdlet
    {
        private const string DisableBrowserIdleSignout = "DisableBrowserIdleSignout";
        private const string EnableBrowserIdleSignout = "EnableBrowserIdleSignout";

        [Parameter(Mandatory = true, ParameterSetName = DisableBrowserIdleSignout)]
        [Parameter(Mandatory = true, ParameterSetName = EnableBrowserIdleSignout)]
        public bool Enabled;

        [Parameter(Mandatory = true, ParameterSetName = EnableBrowserIdleSignout)]
        public TimeSpan WarnAfter;

        [Parameter(Mandatory = true, ParameterSetName = EnableBrowserIdleSignout)]
        public TimeSpan SignoutAfter;

        protected override void ExecuteCmdlet()
        {
            if(Enabled && (!ParameterSpecified(nameof(WarnAfter)) || !ParameterSpecified(nameof(SignoutAfter))))
            {
                throw new PSArgumentException($"{nameof(WarnAfter)} and {nameof(SignoutAfter)} must be specified when enabling the browser idle signout");
            }

            var result = this.Tenant.SetIdleSessionSignOutForUnmanagedDevices(Enabled,WarnAfter,SignoutAfter);
            AdminContext.ExecuteQueryRetry();
        }
    }
}