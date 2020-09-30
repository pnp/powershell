
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Get, "PnPAADUser", DefaultParameterSetName = ParameterSet_LIST)]
    [CmdletMicrosoftGraphApiPermission(MicrosoftGraphApiPermission.User_Read_All | MicrosoftGraphApiPermission.User_ReadWrite_All | MicrosoftGraphApiPermission.Directory_Read_All | MicrosoftGraphApiPermission.Directory_ReadWrite_All)]
    public class GetAADUser : PnPGraphCmdlet
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

        protected override void ExecuteCmdlet()
        {
            if (ParameterSpecified(nameof(Identity)))
            {
                PnP.Framework.Graph.Model.User user;
                if (Guid.TryParse(Identity, out Guid identityGuid))
                {
                    user = PnP.Framework.Graph.UsersUtility.GetUser(AccessToken, identityGuid);
                }
                else
                {
                    user = PnP.Framework.Graph.UsersUtility.GetUser(AccessToken, Identity, Select);
                }
                WriteObject(user);
            }
            else if (ParameterSpecified(nameof(Delta)))
            {
                PnP.Framework.Graph.Model.UserDelta userDelta = PnP.Framework.Graph.UsersUtility.ListUserDelta(AccessToken, DeltaToken, Filter, OrderBy, Select);
                WriteObject(userDelta);
            } 
            else
            {
                List<PnP.Framework.Graph.Model.User> users = PnP.Framework.Graph.UsersUtility.ListUsers(AccessToken, Filter, OrderBy, Select);
                WriteObject(users, true);
            }
        }
    }
}