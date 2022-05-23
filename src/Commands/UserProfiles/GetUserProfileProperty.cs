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

        protected override void ExecuteCmdlet()
        {
            var peopleManager = new PeopleManager(ClientContext);

            foreach (var acc in Account)
            {
                var currentAccount = acc;
                var result = Tenant.EncodeClaim(currentAccount);
                ClientContext.ExecuteQueryRetry();
                currentAccount = result.Value;

                var properties = peopleManager.GetPropertiesFor(currentAccount);
                ClientContext.Load(properties);
                ClientContext.ExecuteQueryRetry();
                WriteObject(properties);
            }
        }
    }
}
