using System.Management.Automation;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;
using PnP.PowerShell.Commands.Base;
using System.Linq;

namespace PnP.PowerShell.Commands.UserProfiles
{
    [Cmdlet(VerbsCommon.Set, "PnPUserProfileProperty")]
    [OutputType(typeof(void))]
    public class SetUserProfileProperty : PnPAdminCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string Account;

        [Parameter(Mandatory = true, ParameterSetName = ParameterAttribute.AllParameterSets)]
        public string PropertyName;

        [Parameter(Mandatory = true, ParameterSetName = "Single")]
        [AllowEmptyString]
        [AllowNull]
        public string Value;

        [Parameter(Mandatory = true,ParameterSetName = "Multi")]
        [AllowEmptyString]
        [AllowNull]
        public string[] Values;

        protected override void ExecuteCmdlet()
        {
            var peopleManager = new PeopleManager(AdminContext);

            var result = Tenant.EncodeClaim(Account);
            AdminContext.ExecuteQueryRetry();

            if (ParameterSetName == "Single")
            {
                peopleManager.SetSingleValueProfileProperty(result.Value, PropertyName, Value);
            }
            else
            {
                peopleManager.SetMultiValuedProfileProperty(result.Value, PropertyName, Values.ToList());
            }

            AdminContext.ExecuteQueryRetry();

        }
    }
}