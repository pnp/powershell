using Microsoft.Graph;
using PnP.Framework.Provisioning.Model.Teams;
using PnP.PowerShell.Commands.Attributes;
using PnP.PowerShell.Commands.Base;
using PnP.PowerShell.Commands.Base.PipeBinds;
using PnP.PowerShell.Commands.Model.Graph;
using PnP.PowerShell.Commands.Model.Teams;
using PnP.PowerShell.Commands.Utilities;
using PnP.PowerShell.Commands.Utilities.REST;
using System;
using System.Linq;
using System.Management.Automation;

namespace PnP.PowerShell.Commands.Teams
{
    [Cmdlet(VerbsCommon.Copy, "PnPTeamsTeam")]
    [RequiredMinimalApiPermissions("Team.Create")]
    public class CopyTeamsTeam : PnPGraphCmdlet
    {
        [Parameter(Mandatory = true)]
        public TeamsTeamPipeBind Identity;

        [Parameter(Mandatory = true)]
        [ValidateNotNullOrEmpty]
        public string DisplayName;

        [Parameter(Mandatory = false)]
        public TeamVisibility Visibility;

        [Parameter(Mandatory = false)]
        public string Classification;
        /**
        * There is a know issue that the mailNickname is currently ignored and cannot be set by the user
        * However the mailNickname is still required by the payload so to deliver better user experience
        * the CLI generates mailNickname for the user 
        * so the user does not have to specify something that will be ignored.
        * For more see: https://learn.microsoft.com/en-us/graph/api/team-clone?view=graph-rest-1.0#request-data
        * This method has to be removed once the graph team fixes the issue and then the actual value
        * of the mailNickname would have to be specified by the CLI user.
        *  [Parameter(Mandatory = true)]
        *   public string MailNickName;
        */
        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public ClonableTeamParts[] PartsToClone;

        protected override void ExecuteCmdlet()
        {
            var groupId = Identity.GetGroupId(this, Connection, AccessToken);
            
            if (groupId == null)
            {
                throw new PSArgumentException("Team not found", nameof(Identity));
            }

            if (!ParameterSpecified(nameof(PartsToClone)))
            {
                // If no specific parts have been provided, all available parts will be copied
                PartsToClone = Enum.GetValues(typeof(ClonableTeamParts)).Cast<ClonableTeamParts>().ToArray();
            }

            TeamCloneInformation teamClone = new TeamCloneInformation();
            teamClone.Classification = Classification;
            teamClone.Description = Description;
            teamClone.DisplayName = DisplayName;
            teamClone.PartsToClone = PartsToClone;
            /** copying displayName into MailNickName still required by the payload so to deliver better user experience
            * but currently ignored and can't be set by user */
            teamClone.MailNickName = DisplayName;
            teamClone.Visibility = (GroupVisibility)Enum.Parse(typeof(GroupVisibility), Visibility.ToString());
            TeamsUtility.CloneTeam(this, AccessToken, Connection, groupId, teamClone);
        }
    }
}
