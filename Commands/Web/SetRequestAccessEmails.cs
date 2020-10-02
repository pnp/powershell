using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;

using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "RequestAccessEmails")]
    public class SetRequestAccessEmails : PnPWebCmdlet
    {
        // Parameter must remain a string array for backwards compatibility, even though only one e-mail address can be provided
        [Parameter(Mandatory = false)]
        public string[] Emails = null;

        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled;

        protected override void ExecuteCmdlet()
        {
            SelectedWeb.EnsureProperty(w => w.HasUniqueRoleAssignments);

            // Can only set the Request Access Emails if the web has unique permissions
            if (SelectedWeb.HasUniqueRoleAssignments)
            {
                if (Emails != null && Emails.Length > 0 && !Disabled)
                {
                    if (Emails.Length > 1)
                    {
                        // Only one e-mail address can be configured to receive the access requests
                        throw new ArgumentException(Resources.SetRequestAccessEmailsOnlyOneAddressAllowed, nameof(Emails));
                    }
                    else
                    {
                        // Configure the one e-mail address to receive the access requests
                        SelectedWeb.SetUseAccessRequestDefaultAndUpdate(false);
                        SelectedWeb.EnableRequestAccess(Emails[0]);
                    }
                }
                else
                {
                    if (Disabled)
                    {
                        // Disable requesting access
                        SelectedWeb.DisableRequestAccess();
                    }
                    else
                    {
                        // Enable requesting access and set it to the default owners group
                        // Code can be replaced by SelectedWeb.EnableRequestAccess(); once https://github.com/SharePoint/PnP-Sites-Core/pull/2533 has been accepted for merge.
                        SelectedWeb.SetUseAccessRequestDefaultAndUpdate(true);
                        SelectedWeb.Update();
                        SelectedWeb.Context.ExecuteQueryRetry();
                    }
                }
            }
        }
    }
}