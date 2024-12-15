using System;
using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Resources = PnP.PowerShell.Commands.Properties.Resources;

namespace PnP.PowerShell.Commands
{
    [Cmdlet(VerbsCommon.Set, "PnPRequestAccessEmails")]
    [OutputType(typeof(void))]
    public class SetRequestAccessEmails : PnPWebCmdlet
    {
        // Parameter must remain a string array for backwards compatibility, even though only one e-mail address can be provided
        [Parameter(Mandatory = false)]
        public string[] Emails = null;

        [Parameter(Mandatory = false)]
        public SwitchParameter Disabled;

        protected override void ExecuteCmdlet()
        {
            CurrentWeb.EnsureProperty(w => w.HasUniqueRoleAssignments);

            // Can only set the Request Access Emails if the web has unique permissions
            if (CurrentWeb.HasUniqueRoleAssignments)
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
                        CurrentWeb.SetUseAccessRequestDefaultAndUpdate(false);
                        CurrentWeb.EnableRequestAccess(Emails[0]);
                    }
                }
                else
                {
                    if (Disabled)
                    {
                        // Disable requesting access
                        CurrentWeb.DisableRequestAccess();
                    }
                    else
                    {
                        // Enable requesting access and set it to the default owners group
                        CurrentWeb.EnableRequestAccess();
                    }
                }
            }
        }
    }
}