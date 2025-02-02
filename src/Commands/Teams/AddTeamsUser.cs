using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Add, "PnPTeamsUser")]
    [RequiredApiDelegatedOrApplicationPermissions("graph/Group.ReadWrite.All")]
    public class AddTeamsUser : PnPGraphCmdlet
    {
        const string ParamSet_ByUser = "By User";
        const string ParamSet_ByMultipleUsers = "By Multiple Users";

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByUser)]
        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByMultipleUsers)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = false, ParameterSetName = ParamSet_ByUser)]
        public TeamsChannelPipeBind Channel;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByUser)]
        public string User;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByMultipleUsers)]
        public string[] Users;

        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByUser)]
        [Parameter(Mandatory = true, ParameterSetName = ParamSet_ByMultipleUsers)]
        [ValidateSet(new[] { "Owner", "Member" })]
        public string Role;
        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(GraphRequestHelper);
            if (groupId != null)
            {
                try
                {
                    if (ParameterSpecified(nameof(Channel)))
                    {
                        var channelId = Channel.GetId(GraphRequestHelper, groupId);
                        if (channelId == null)
                        {
                            throw new PSArgumentException("Channel not found");
                        }
                        TeamsUtility.AddChannelMember(GraphRequestHelper, groupId, channelId, User, Role);
                    }
                    else
                    {
                        if (ParameterSetName == ParamSet_ByUser)
                        {
                            TeamsUtility.AddUser(GraphRequestHelper, groupId, User, Role);
                        }
                        else
                        {
                            TeamsUtility.AddUsers(GraphRequestHelper, groupId, Users, Role);
                        }
                    }
                }
                catch (GraphException ex)
                {
                    if (ex.Error != null)
                    {
                        throw new PSInvalidOperationException(ex.Error.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                throw new PSArgumentException("Group not found");
            }
        }
    }
}
