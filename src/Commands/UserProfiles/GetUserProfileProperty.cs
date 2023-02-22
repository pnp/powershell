using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;

using PnP.PowerShell.Commands.Base;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPUserProfileProperty")]
    [OutputType(typeof(PersonProperties))]
    public class GetUserProfileProperty : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string[] Account;

        [Parameter(Mandatory = false)]
        public string[] Properties;

        protected override void ExecuteCmdlet()
        {
            var peopleManager = new PeopleManager(ClientContext);

            foreach (var acc in Account)
            {
                var currentAccount = acc;
                var result = Tenant.EncodeClaim(currentAccount);
                ClientContext.ExecuteQueryRetry();
                currentAccount = result.Value;

                if (ParameterSpecified(nameof(Properties)) && Properties != null && Properties.Length > 0)
                {
                    UserProfilePropertiesForUser userProfilePropertiesForUser = new UserProfilePropertiesForUser(ClientContext, currentAccount, Properties);
                    var properties = peopleManager.GetUserProfilePropertiesFor(userProfilePropertiesForUser);
                    ClientContext.Load(userProfilePropertiesForUser);
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(properties, true);

                }
                else
                {
                    var userProfileProperties = peopleManager.GetPropertiesFor(currentAccount);
                    ClientContext.Load(userProfileProperties);
                    ClientContext.ExecuteQueryRetry();
                    WriteObject(userProfileProperties);
                }

            }
        }
    }
}
