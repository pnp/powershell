using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;
using System;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsCommon.Get, "PnPBrowserIdleSignout")]
    public class GetBrowserIdleSignout : PnPSharePointOnlineAdminCmdlet
    {
        protected override void ExecuteCmdlet()
        {
            var enabled = false;
            TimeSpan warnAfter = TimeSpan.Zero;
            TimeSpan signOutAfter = TimeSpan.Zero;
            
            var result = this.Tenant.GetIdleSessionSignOutForUnmanagedDevices();
            AdminContext.ExecuteQueryRetry();

            try
            {
                var splittedValue = result.Value.Split(new [] {","}, 3, System.StringSplitOptions.None);
                if (splittedValue.Length == 1 && !bool.Parse(splittedValue[0]))
                {
                    enabled = false;
                }
                else
                {
                    if (splittedValue.Length != 3)
                    {
                        throw new PSInvalidOperationException("Incorrect value returned");
                    }
                    enabled = bool.Parse(splittedValue[0]);
                    warnAfter = TimeSpan.FromSeconds(long.Parse(splittedValue[1]));
                    signOutAfter = TimeSpan.FromSeconds(long.Parse(splittedValue[2]));
                }
            }
            catch
            {
                throw new PSInvalidOperationException("Parsing error");
            }

            var returnObject = new PSObject();
            returnObject.Properties.Add(new PSNoteProperty("Enabled", enabled));
            returnObject.Properties.Add(new PSNoteProperty("WarnAfter", warnAfter));
            returnObject.Properties.Add(new PSNoteProperty("SignOutAfter", signOutAfter));

            WriteObject(returnObject);
        }
    }
}