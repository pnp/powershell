using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPUserSession", SupportsShouldProcess = true)]
    public class RevokeUserSession : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNull]
        public string User;
        protected override void ExecuteCmdlet()
        {
            if (ShouldProcess($"Sign out user {User} from all devices"))
            {
                var office365Tenant = new Office365Tenant(AdminContext);

                var result = office365Tenant.RevokeAllUserSessions(User);
                AdminContext.Load(result);
                AdminContext.ExecuteQueryRetry();
                switch (result.State)
                {
                    case SPOUserSessionRevocationState.FeatureDisabled:
                        {
                            WriteWarning("This cmdlet will be available in the future, but is not ready for use in your organization yet.");
                            break;
                        }
                    case SPOUserSessionRevocationState.Failure:
                        {
                            WriteWarning($"Sorry, something went wrong and we could not sign out {User} from any device.");
                            break;
                        }
                    case SPOUserSessionRevocationState.InstantaneousSuccess:
                        {
                            WriteWarning($"We succesfully signed out {User} from all devices.");
                            break;
                        }
                    case SPOUserSessionRevocationState.NonInstantaneousSuccess:
                        {
                            WriteWarning($"It can take up to an hour to sign out {User} from all devices.");
                            break;
                        }
                    case SPOUserSessionRevocationState.UserNotFound:
                        {
                            WriteWarning($"We could not find the user {User}. Check for typos and try again.");
                            break;
                        }
                    default:
                        throw new PSInvalidOperationException();
                }
            }
        }
    }
}