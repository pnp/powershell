
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Utilities.EntraID;
using System;
using System.Management.Automation;
using System.Net;

namespace PnP.PowerShell.Commands.Principals
{
    [Cmdlet(VerbsCommon.Get, "PnPEntraIDUser", DefaultParameterSetName = ParameterSet_LIST)]
    [RequiredMinimalApiPermissions("User.Read.All")]
    [Alias("Get-PnPAzureADUser")]
    public class GetEntraIDUser : PnPGraphCmdlet
    {
        const string ParameterSet_BYID = "Return by specific ID";
        const string ParameterSet_LIST = "Return a list";
        const string ParameterSet_DELTA = "Return the delta";

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        public string Identity;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public string Filter;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public string OrderBy;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public string[] Select;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSet_DELTA)]
        public SwitchParameter Delta;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public string DeltaToken;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public int StartIndex = 0;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public int? EndIndex = null;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_BYID)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_LIST)]
        public SwitchParameter IgnoreDefaultProperties;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSet_DELTA)]
        public SwitchParameter UseBeta;        

        protected override void ExecuteCmdlet()
        {
            if (Connection.ClientId == PnPConnection.PnPManagementShellClientId)
            {
                Connection.Scopes = new[] { "Directory.ReadWrite.All" };
            }
            
            if(ParameterSpecified(nameof(IgnoreDefaultProperties)) && !ParameterSpecified(nameof(Select)))
            {
                throw new ArgumentException($"When providing {nameof(IgnoreDefaultProperties)}, you must provide {nameof(Select)}", nameof(Select));
            }

            if (ParameterSpecified(nameof(Identity)))
            {
                PnP.PowerShell.Commands.Model.AzureAD.User user;
                if (Guid.TryParse(Identity, out Guid identityGuid))
                {
                    user = UsersUtility.GetUser(AccessToken, identityGuid, ignoreDefaultProperties: IgnoreDefaultProperties, useBetaEndPoint: UseBeta.IsPresent);
                }
                else
                {
                    user = UsersUtility.GetUser(AccessToken, WebUtility.UrlEncode(Identity), Select, ignoreDefaultProperties: IgnoreDefaultProperties, useBetaEndPoint: UseBeta.IsPresent);
                }
                WriteObject(user);
            }
            else if (ParameterSpecified(nameof(Delta)))
            {
                var userDelta = UsersUtility.ListUserDelta(AccessToken, DeltaToken, Filter, OrderBy, Select, StartIndex, EndIndex, useBetaEndPoint: UseBeta.IsPresent);
                WriteObject(userDelta);
            } 
            else
            {
                var users = UsersUtility.ListUsers(AccessToken, Filter, OrderBy, Select, ignoreDefaultProperties: IgnoreDefaultProperties, StartIndex, EndIndex, useBetaEndPoint: UseBeta.IsPresent);
                WriteObject(users, true);
            }
        }
    }
}