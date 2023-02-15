using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Enums;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Utilities;
using System;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Graph
{
    [Cmdlet(VerbsCommon.Add, "PnPTeamsChannel", DefaultParameterSetName = ParameterSET_STANDARD)]
    [RequiredMinimalApiPermissions("Group.ReadWrite.All")]
    public class AddTeamsChannel : PnPGraphCmdlet
    {
        private const string ParameterSET_PRIVATE = "Private channel type";
        private const string ParameterSET_STANDARD = "Standard channel type";
        private const string ParameterSET_SPECIFIC = "Specific channel type";

        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_STANDARD)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PRIVATE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_SPECIFIC)]
        public TeamsTeamPipeBind Team;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_STANDARD)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PRIVATE)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_SPECIFIC)]
        public string DisplayName;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSET_STANDARD)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSET_PRIVATE)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSET_SPECIFIC)]
        public string Description;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PRIVATE)]
        [Obsolete($"Use {nameof(ChannelType)} instead.")]
        public SwitchParameter Private;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSET_STANDARD)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_SPECIFIC)]
        public TeamsChannelType ChannelType = TeamsChannelType.Standard;

        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_SPECIFIC)]
        [Parameter(Mandatory = true, ParameterSetName = ParameterSET_PRIVATE)]
        public string OwnerUPN;

        [Parameter(Mandatory = false, ParameterSetName = ParameterSET_SPECIFIC)]
        [Parameter(Mandatory = false, ParameterSetName = ParameterSET_STANDARD)]
        public bool IsFavoriteByDefault;

        protected override void ExecuteCmdlet()
        {
            var groupId = Team.GetGroupId(Connection, AccessToken);
            if (groupId == null)
            {
                throw new PSArgumentException("Group not found");
            }

            if(ChannelType != TeamsChannelType.Standard && !ParameterSpecified(nameof(OwnerUPN)))
            {
                throw new PSArgumentException("OwnerUPN is required when using the non standard channel type", nameof(OwnerUPN));
            }

            try
            {
                var channel = TeamsUtility.AddChannelAsync(AccessToken, Connection, groupId, DisplayName, Description, ChannelType, OwnerUPN, IsFavoriteByDefault).GetAwaiter().GetResult();
                WriteObject(channel);
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
    }
}
