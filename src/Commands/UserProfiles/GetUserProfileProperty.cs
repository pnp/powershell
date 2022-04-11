using System.Management.Automation;

using Microsoft.Online.SharePoint.TenantAdministration;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;

using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Get, "PnPUserProfileProperty", DefaultParameterSetName = PARAMETERSET_CURRENTUSER)]
    [OutputType(typeof(PersonProperties))]
    public class GetUserProfileProperty : PnPAdminCmdlet
    {
        protected override bool ImplicitAdminContextSwitch => true;

        private const string PARAMETERSET_ACCOUNT_ADMIN = "By Account (Tenant Admin)";
        private const string PARAMETERSET_CLAIMS = "By Claims";
        private const string PARAMETERSET_CURRENTUSER = "Current User";
        private const string PARAMETERSET_USER = "By User";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PARAMETERSET_ACCOUNT_ADMIN)]
        public string[] Account;

        protected override void ExecuteCmdlet()
        {
            switch (ParameterSetName)
            {
                case PARAMETERSET_ACCOUNT_ADMIN:
                    ByAccount();
                    break;

                case PARAMETERSET_CLAIMS:
                    ByClaims();
                    break;

                case PARAMETERSET_CURRENTUSER:
                    ByCurrentUser();
                    break;

                case PARAMETERSET_USER:
                    ByUser();
                    break;

                default:
                    throw new PSArgumentException(nameof(ParameterSetName));
            }
        }

        private void ByAccount()
        {
            SwitchToAdminClientContext();

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

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = PARAMETERSET_CLAIMS)]
        public string Claims { get; set; }

        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_CLAIMS)]
        [Parameter(Mandatory = false, ParameterSetName = PARAMETERSET_CURRENTUSER)]
        public SwitchParameter FromTenantAdminSite { get; set; }

        private void ByClaims()
        {
            if (FromTenantAdminSite)
            {
                SwitchToAdminClientContext();
            }

            var peopleManager = new PeopleManager(ClientContext);

            var properties = peopleManager.GetPropertiesFor(Claims);
            ClientContext.Load(properties);
            ClientContext.ExecuteQueryRetry();
            WriteObject(properties);
        }

        private void ByCurrentUser()
        {
            if (FromTenantAdminSite)
            {
                SwitchToAdminClientContext();
            }

            var peopleManager = new PeopleManager(ClientContext);
            var properties = peopleManager.GetMyProperties();
            ClientContext.Load(properties);
            ClientContext.ExecuteQueryRetry();
            WriteObject(properties);
        }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = PARAMETERSET_USER)]
        public UserPipeBind User { get; set; }

        private void ByUser()
        {
            var user = User.GetUser(ClientContext);

            if (user is null)
            {
                WriteWarning($"User not found - '{User}'");
                return;
            }

            var loginName = user.LoginName;

            if (string.IsNullOrEmpty(loginName))
            {
                WriteWarning($"Login name of User ID {user.Id} '{user.Title}' is null or empty");
                return;
            }

            var peopleManager = new PeopleManager(ClientContext);
            var properties = peopleManager.GetPropertiesFor(loginName);
            ClientContext.Load(properties);
            ClientContext.ExecuteQueryRetry();
            WriteObject(properties);
        }
    }
}
