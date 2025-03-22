using Microsoft.Online.SharePoint.TenantManagement;
using Microsoft.SharePoint.Client;
using PnP.PowerShell.Commands.Base;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Admin
{
    [Cmdlet(VerbsSecurity.Revoke, "PnPUserSession")]
    public class RevokeUserSession : PnPSharePointOnlineAdminCmdlet
    {
        [Parameter(Mandatory = true)]
        [ValidateNotNull]
        public string User;
        protected override void ExecuteCmdlet()
        {
            if (ShouldContinue($"Sign out user {User} from all devices ?", Properties.Resources.Confirm))
            {
                var office365Tenant = new Office365Tenant(AdminContext);

                var result = office365Tenant.RevokeAllUserSessions(User);
                AdminContext.Load(result);
                AdminContext.ExecuteQueryRetry();
                switch (result.State)
                {
                    case SPOUserSessionRevocationState.FeatureDisabled:
                        {
                            LogWarning("This cmdlet will be available in the future, but is not ready for use in your organization yet.");
                            break;
                        }
                    case SPOUserSessionRevocationState.Failure:
                        {
                            LogWarning($"Sorry, something went wrong and we could not sign out {User} from any device.");
                            break;
                        }
                    case SPOUserSessionRevocationState.InstantaneousSuccess:
                        {
                            LogWarning($"We succesfully signed out {User} from all devices.");
                            break;
                        }
                    case SPOUserSessionRevocationState.NonInstantaneousSuccess:
                        {
                            LogWarning($"It can take up to an hour to sign out {User} from all devices.");
                            break;
                        }
                    case SPOUserSessionRevocationState.UserNotFound:
                        {
                            LogWarning($"We could not find the user {User}. Check for typos and try again.");
                            break;
                        }
                    default:
                        throw new PSInvalidOperationException();
                }
            }
        }
    }
}